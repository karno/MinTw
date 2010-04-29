using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Drawing;

namespace Mintw
{
    public class Config
    {
        public Config()
        {
            Application.ThreadExit += new EventHandler(Application_ThreadExit);
        }

        void Application_ThreadExit(object sender, EventArgs e)
        {
            Save();
        }

        /// <summary>
        /// 設定ファイルのパス
        /// </summary>
        private static string filePath = "mintw.xml";
        private static string GetFilePath()
        {
            return Path.Combine(Application.StartupPath, filePath);
        }

        /// <summary>
        /// 設定を読み込み
        /// </summary>
        public static Config Load()
        {
            var path = GetFilePath();
            if (File.Exists(path))
                return K.Snippets.Files.LoadXML<Config>(path, true);
            else
                return new Config();
        }

        /// <summary>
        /// 設定を保存
        /// </summary>
        public void Save()
        {
            K.Snippets.Files.SaveXML<Config>(GetFilePath(), this);
        }

        /// <summary>
        /// ユーザーID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// ユーザートークン
        /// </summary>
        public string UserToken { get; set; }
        /// <summary>
        /// ユーザーシークレット
        /// </summary>
        public ProtectedString UserSecret { get; set; }

        /// <summary>
        /// Mentionチェックをするか
        /// </summary>
        public bool MentionEnabled = true;
        /// <summary>
        /// Mentionチェックの間隔
        /// </summary>
        public int MentionInverval = 1;

        /// <summary>
        /// DMチェックが有効か
        /// </summary>
        public bool DMEnabled = true;
        /// <summary>
        /// DMチェックの間隔
        /// </summary>
        public int DMInterval = 5;

        #region UrlCompress
        /// <summary>
        /// URL短縮サービスを利用するか
        /// </summary>
        public bool UrlCompress = false;

        /// <summary>
        /// j.mpのID
        /// </summary>
        public string JmpId = null;

        /// <summary>
        /// j.mpのパスワード
        /// </summary>
        public ProtectedString JmpPass = null;

        #endregion

        #region PictureUpload

        /// <summary>
        /// 画像のアップロードサービスを利用するか
        /// </summary>
        public bool PictureUpload = false;

        /// <summary>
        /// TwitPicのID
        /// </summary>
        public string TwitpicId = null;

        /// <summary>
        /// TwitPicのパスワード
        /// </summary>
        public ProtectedString TwitpicPass = null;

        #endregion

        #region PostWindowConfiguration

        /// <summary>
        /// 投稿ウィンドウのサイズ
        /// </summary>
        public Size PostWindowSize = new Size(450, 120);

        /// <summary>
        /// 自動URL短縮をするか
        /// </summary>
        public bool AutoShorten = true;

        /// <summary>
        /// ユーザーIDの入力補助を使うか
        /// </summary>
        public bool UserIdSuggest = true;

        #endregion

        #region StateInfo

        /// <summary>
        /// 最後に受信したMentionのID
        /// </summary>
        public long PrevReceivedMentionId = 0;

        /// <summary>
        /// 最後に受信したDMのID
        /// </summary>
        public long PrevReceivedDmId = 0;

        #endregion

        #region Notification

        /// <summary>
        /// Count of show digest status
        /// </summary>
        public int StatusDigestCount = 3;

        /// <summary>
        /// Length of digest text max
        /// </summary>
        public int TextLength = 20;

        /// <summary>
        /// Digest format string
        /// </summary>
        public string DigestFormat = "{0}: {2}";
        #endregion
    }

    /// <summary>
    /// 暗号化された文字列の保持クラス
    /// </summary>
    public class ProtectedString
    {
        public static implicit operator String(ProtectedString s)
        {
            if (s == null)
                return null;
            return s.Value;
        }

        public static implicit operator ProtectedString(string s)
        {
            return new ProtectedString(s);
        }

        public ProtectedString() : this(null) { }

        public ProtectedString(string val)
        {
            Value = val;
        }

        [XmlIgnore()]
        public string Value { get; set; }

        public string EncryptedValue
        {
            get
            {
                if (Value == null)
                    return null;
                return ConfigHelper.EncryptString(this.Value);
            }
            set
            {
                if (value != null)
                {
                    try
                    {
                        this.Value = ConfigHelper.DecryptString(value);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Decryption error:" + e.Message);
                    }
                }
            }
        }
    }

    public static class ConfigHelper
    {
        private static string IVKey = "820d85c41de3e35e7bb9788d1d66268b";
        private static string KeyIV = "81c412c41b76a36575839c49933b6f0b";

        public static string EncryptString(string str)
        {
            byte[] bytesIn = System.Text.Encoding.UTF8.GetBytes(str);

            var aes = new AesManaged();

            byte[] bytesKey = Encoding.UTF8.GetBytes(IVKey);
            byte[] bytesIV = Encoding.UTF8.GetBytes(KeyIV);
            aes.Key = FormatBytes(bytesIV, aes.Key.Length);
            aes.IV = FormatBytes(bytesKey, aes.IV.Length);

            var enc = aes.CreateEncryptor();
            using (var ms = new System.IO.MemoryStream())
            using (var cs = new CryptoStream(ms, enc, CryptoStreamMode.Write))
            {
                cs.Write(bytesIn, 0, bytesIn.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static string DecryptString(string str)
        {
            byte[] bytesIn = System.Text.Encoding.UTF8.GetBytes(str);

            var aes = new AesManaged();

            byte[] bytesKey = Encoding.UTF8.GetBytes(IVKey);
            byte[] bytesIV = Encoding.UTF8.GetBytes(KeyIV);
            aes.Key = FormatBytes(bytesIV, aes.Key.Length);
            aes.IV = FormatBytes(bytesKey, aes.IV.Length);

            byte[] bytes = Convert.FromBase64String(str);

            var decryptor = aes.CreateDecryptor();
            using (var ms = new MemoryStream(bytes))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var sr = new StreamReader(cs, Encoding.UTF8))
            {
                return sr.ReadToEnd();
            }
        }

        private static byte[] FormatBytes(byte[] bytes, int len)
        {
            byte[] newBytes = new byte[len];
            if (bytes.Length <= len)
            {
                for (int i = 0; i < bytes.Length; i++)
                    newBytes[i] = bytes[i];
            }
            else
            {
                int pos = 0;
                for (int i = 0; i < bytes.Length; i++)
                {
                    newBytes[pos++] ^= bytes[i];
                    if (pos >= newBytes.Length)
                        pos = 0;
                }
            }
            return newBytes;
        }
    }
}