using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using Std.Network;
using System.Windows.Forms;

namespace Mintw
{
    public class TwitPicUpload
    {
        protected virtual Uri UploadUrl { get { return new Uri("http://twitpic.com/api/upload"); } }

        /// <summary>
        /// 画像をアップロード
        /// </summary>
        /// <param name="path">アップロードするファイルのパス</param>
        /// <param name="id">ID</param>
        /// <param name="pw">パスワード</param>
        /// <returns>アップロードしたURL</returns>
        public string UploadImage(string path, string id, string pw)
        {
            List<Http.SendData> data = new List<Http.SendData>();
            data.Add(new Http.SendData("username", id));
            data.Add(new Http.SendData("password", pw));
            data.Add(new Http.SendData("media", path, true));
            var req = Http.CreateRequest(UploadUrl, false);
            var r = Http.WebUpload<string>(
                req,
                null,
                CallbackReader,
                data.ToArray(),
                Encoding.UTF8);
            if (r.Succeeded)
                return r.Data;
            else
                return null;
        }

        private string CallbackReader(Stream s)
        {
            using (var xr = XmlReader.Create(s))
            {
                var doc = XDocument.Load(xr);
                var r = doc.Element("rsp");
                var a = r.Attribute("status");
                bool ok = false;
                if (a != null && a.Value == "ok")
                {
                    ok = true;
                }
                else
                {
                    a = r.Attribute("stat");
                    if (a != null && a.Value == "ok")
                    {
                        ok = true;
                    }
                }
                if (ok)
                {
                    return r.Element("mediaurl").Value;
                }
                else
                {
                    MessageBox.Show("Upload falled.");
                    return null;
                }
            }

        }
    }
}
