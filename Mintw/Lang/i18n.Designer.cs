﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:2.0.50727.4927
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mintw.Lang {
    using System;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class i18n {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal i18n() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Mintw.Lang.i18n", typeof(i18n).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   厳密に型指定されたこのリソース クラスを使用して、すべての検索リソースに対し、
        ///   現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   {0} new mentions and DMs received. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string DMReceived {
            get {
                return ResourceManager.GetString("DMReceived", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Errors has occured. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string OnHandledError {
            get {
                return ResourceManager.GetString("OnHandledError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   {0} new mentions received. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string Received {
            get {
                return ResourceManager.GetString("Received", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Some error has occured in updating. に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string UpdateError {
            get {
                return ResourceManager.GetString("UpdateError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Update error に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string UpdateErrorTitle {
            get {
                return ResourceManager.GetString("UpdateErrorTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   &quot;{0}&quot; is probably not URL.
        ///Are you sure to shorten it? に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string URLCheckNotify {
            get {
                return ResourceManager.GetString("URLCheckNotify", resourceCulture);
            }
        }
        
        /// <summary>
        ///   URL shortening warning に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string URLCheckNotifyTitle {
            get {
                return ResourceManager.GetString("URLCheckNotifyTitle", resourceCulture);
            }
        }
    }
}