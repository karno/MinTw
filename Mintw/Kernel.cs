using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Std.Tweak;
using System.Threading;

namespace Mintw
{
    public static class Kernel
    {
        /// <summary>
        /// 設定保持クラス
        /// </summary>
        public static Config Config = Config.Load();

        /// <summary>
        /// フォロー中のユーザー
        /// </summary>
        public static TwitterUser[] Followings = null;

        /// <summary>
        /// 一分ごとにアクセサを呼ぶタイマー
        /// </summary>
        public static Timer MinitTimer = null;

        /// <summary>
        /// アクセサ
        /// </summary>
        public static Access Accessor = new Access();

        /// <summary>
        /// Tweetウィンドウ
        /// </summary>
        public static Tweet TweetWindow = new Tweet();

        /// <summary>
        /// URL圧縮
        /// </summary>
        public static JmpUrlCompress JmpUrlCompressor = new JmpUrlCompress();

        /// <summary>
        /// 画像アップロード
        /// </summary>
        public static TwitPicUpload TwitPic = new TwitPicUpload();

    }
}
