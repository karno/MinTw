using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Std.Network
{
    //Singleton Instance
    public static class Http
    {
        /// <summary>
        /// If-Modified-Since dateTime dictionary
        /// </summary>
        private static Dictionary<Uri, DateTime> modifieds = new Dictionary<Uri, DateTime>();

        public static void ClearModifiedsCache()
        {
            modifieds.Clear();
        }

        public static string UserAgent = "Std/HttpLib 1.0(.NET Framework 3.5)";

        public static int TimeoutInterval = 5000;

        public static bool UseExpect100ContinueAsDefault = false;

        public static IWebProxy DefaultProxy = WebRequest.DefaultWebProxy;

        /// <summary>
        /// Create Web request
        /// </summary>
        /// <param name="uri">Target URI</param>
        /// <param name="usemodifieds">Use If-Modified-Since Header</param>
        public static HttpWebRequest CreateRequest(Uri uri, bool usemodifieds)
        {
            if (uri == null)
                throw new NullReferenceException("CreateRequest::URI is NULL.");
            //Create request
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);

            //HTTP:100 Continue
            req.ServicePoint.Expect100Continue = UseExpect100ContinueAsDefault;

            //UA
            req.UserAgent = UserAgent;

            //Timeout
            req.Timeout = TimeoutInterval;

            //Compression
            req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;


            //If-Modified-Since
            if (usemodifieds)
            {
                if (modifieds.ContainsKey(uri))
                {
                    req.IfModifiedSince = modifieds[uri];
                    modifieds[uri] = DateTime.Now;
                }
                else
                {
                    modifieds.Add(uri, DateTime.Now);
                }
            }

            req.ContentType = "application/x-www-form-urlencoded";

            //default GET
            req.Method = "GET";

            return req;
        }

        /// <summary>
        /// Connect network with default HTTPWebRequest
        /// </summary>
        /// <param name="uri">Target URI</param>
        /// <param name="usemodifieds">Use If-Modified-Since Header</param>
        /// <param name="method">Using method(ex:GET,POST,etc...)</param>
        /// <summary>
        /// Delegate for stream callback
        /// </summary>
        public delegate T DStreamCallback<T>(Stream strm);

        /// <summary>
        /// delegate for stream callback full-type
        /// </summary>
        public delegate T DStreamCallbackFull<T>(WebResponse res);

        public static OperationResult<T> WebConnect<T>
            (HttpWebRequest req,
            string method,
            ICredentials credential,
            DStreamCallback<T> callback)
        {
            return WebConnect<T>(req, method, credential, callback, null);
        }


        /// <summary>
        /// Connect Network with manual HTTPWebRequest
        /// </summary>
        /// <param name="req">Using HttpWebRequest</param>
        /// <param name="method">Using method(ex:GET,POST,etc...)</param>
        /// <param name="credential">Using credential information</param>
        /// <param name="callback">Stream callback</param>
        /// <param name="senddata">Sending data(if you use this arg, you MUST use "POST" method)</param>
        public static OperationResult<T> WebConnect<T>
            (HttpWebRequest req,
            string method,
            ICredentials credential,
            DStreamCallback<T> callback,
            byte[] senddata)
        {
            return WebConnect<T>(req, method, credential, callback, null, senddata);
        }


        /// <summary>
        /// Connect Network with manual HTTPWebRequest
        /// </summary>
        /// <param name="req">Using HttpWebRequest</param>
        /// <param name="method">Using method(ex:GET,POST,etc...)</param>
        /// <param name="credential">Using credential information</param>
        /// <param name="callback">Stream callback</param>
        public static OperationResult<T> WebConnect<T>
            (HttpWebRequest req,
            string method,
            ICredentials credential,
            DStreamCallbackFull<T> callback)
        {
            return WebConnect<T>(req, method, credential, null, callback, null);
        }
        
        /// <summary>
        /// Connect Network with manual HTTPWebRequest
        /// </summary>
        /// <param name="req">Using HttpWebRequest</param>
        /// <param name="method">Using method(ex:GET,POST,etc...)</param>
        /// <param name="credential">Using credential information</param>
        /// <param name="callback">Stream callback</param>
        /// <param name="senddata">Sending data(if you use this arg, you MUST use "POST" method)</param>
        public static OperationResult<T> WebConnect<T>
            (HttpWebRequest req,
            string method,
            ICredentials credential,
            DStreamCallbackFull<T> callback,
            byte[] senddata)
        {
            return WebConnect<T>(req, method, credential, null, callback, senddata);
        }

        /// <summary>
        /// Base implementation for web connect
        /// </summary>
        private static OperationResult<T> WebConnect<T>
            (HttpWebRequest req,
            string method,
            ICredentials credential,
            DStreamCallback<T> callback,
            DStreamCallbackFull<T> callbackFull,
            byte[] senddata)
        {
            try
            {
                //Set method
                req.Method = method;

                //Set credential
                if (credential != null)
                    req.Credentials = credential;

                if (senddata != null && senddata.Length != 0)
                {
                    //Set sending-length
                    req.ContentLength = senddata.Length;

                    using (Stream s = req.GetRequestStream())
                    {
                        s.Write(senddata, 0, senddata.Length);
                    }
                }
                using (var res = req.GetResponse())
                    return TreatWebResponse<T>((HttpWebResponse)res, callback, callbackFull);
            }
            catch (Exception e)
            {
                return new OperationResult<T>(req.RequestUri, e);
            }
        }

        /// <summary>
        /// Request to web forms, and read responce with string.
        /// </summary>
        /// <param name="req">request</param>
        /// <param name="values">send values</param>
        /// <param name="encode">encoding</param>
        /// <returns>OperationResult</returns>
        public static OperationResult<string> WebFormSendString(
            HttpWebRequest req,
            Dictionary<string, string> values,
            Encoding encode
            )
        {
            return WebFormSendString<string>
                (req,
                CommonStreamReader.ReadStringByStream,
                values,
                encode);
        }

        /// <summary>
        /// Request to web forms.<para />
        /// (use x-www-form-urlencoded)
        /// </summary>
        /// <typeparam name="T">Return type</typeparam>
        /// <param name="req">request</param>
        /// <param name="callback">callback delegate</param>
        /// <param name="sends">send data</param>
        /// <param name="encode">encoder</param>
        /// <returns>OperationResult</returns>
        public static OperationResult<T> WebFormSendString<T>
            (HttpWebRequest req,
            DStreamCallback<T> callback,
            Dictionary<string, string> values,
            Encoding encode)
        {
            var paras = from k in values.Keys
                        select k + "=" + values[k];

            var dat = encode.GetBytes(String.Join("&", paras.ToArray()));
            req.Method = "POST"; //fixed
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = dat.Length;

            try
            {

                // ポスト・データの書き込み
                Stream reqStream = req.GetRequestStream();
                reqStream.Write(dat, 0, dat.Length);
                reqStream.Close();

                WebResponse res = req.GetResponse();
                // レスポンスの読み取り
                using (var rs = res.GetResponseStream())
                {
                    return new OperationResult<T>(req.RequestUri, callback(rs));
                }
            }
            catch (WebException we)
            {
                return new OperationResult<T>(req.RequestUri, we);
            }
        }

        /// <summary>
        /// Upload data for network with manual HTTPWebRequest and multipart/form-data.(this method always use "POST")
        /// </summary>
        /// <param name="req">Using HttpWebRequest</param>
        /// <param name="credential">Using credential information</param>
        /// <param name="callback">Stream callback</param>
        /// <param name="senddata">Sending data</param>
        /// <param name="encode">Using encode</param>
        public static OperationResult<T> WebUpload<T>
            (HttpWebRequest req,
            ICredentials credential,
            DStreamCallback<T> callback,
            SendData[] senddata,
            Encoding encode)
        {
            return WebUpload<T>(req, credential, callback, null, senddata, encode);
        }

        /// <summary>
        /// Upload data for network with manual HTTPWebRequest and multipart/form-data.(this method always use "POST")
        /// </summary>
        /// <param name="req">Using HttpWebRequest</param>
        /// <param name="credential">Using credential information</param>
        /// <param name="callback">Stream callback</param>
        /// <param name="senddata">Sending data</param>
        /// <param name="encode">Using encode</param>
        public static OperationResult<T> WebUpload<T>
            (HttpWebRequest req,
            ICredentials credential,
            DStreamCallbackFull<T> callback,
            SendData[] senddata,
            Encoding encode)
        {
            return WebUpload<T>(req, credential, null, callback, senddata, encode);
        }

        /// <summary>
        /// Base implementation for web upload
        /// </summary>
        private static OperationResult<T> WebUpload<T>
            (HttpWebRequest req,
            ICredentials credential,
            DStreamCallback<T> callback,
            DStreamCallbackFull<T> callbackFull,
            SendData[] senddata,
            Encoding encode)
        {
            try
            {
                //Boundary
                string boundary = Guid.NewGuid().ToString("N");
                string separator = "--" + boundary + "\r\n";

                //Set method
                req.Method = "POST";
                req.ContentType = "multipart/form-data; boundary=" + boundary;

                //Set credential
                if (credential != null)
                    req.Credentials = credential;

                //Create post-data
                //String-Filename alternative
                List<StringBuilder> sbs = new List<StringBuilder>();
                int idx = -1;
                foreach (SendData send in senddata)
                {
                    if (idx == -1)
                    {
                        sbs.Add(new StringBuilder());
                        idx = 0;
                    }
                    if (send.filemode)
                    {
                        sbs[idx].Append(separator + "Content-Disposition: form-data; name=\"" + send.Name + "\"; filename=\"" + Path.GetFileName(send.textorfilename) + "\"\r\n");
                        sbs[idx].Append("Content-Type: application/octet-stream\r\n");
                        sbs[idx].Append("Content-Transfer-Encoding: binary\r\n\r\n");
                        sbs.Add(new StringBuilder());
                        idx++;
                        sbs[idx].Append(send.textorfilename);
                        sbs.Add(new StringBuilder());
                        idx++;
                    }
                    else
                    {
                        sbs[idx].Append(separator + "Content-Disposition: form-data; name=\"" + send.Name + "\"\r\n\r\n");
                        sbs[idx].Append(send.textorfilename + "\r\n");
                    }
                }
                sbs[idx].Append("\r\n--" + boundary + "--\r\n");


                //Check length
                long gross = 0;
                for (int c = 0; c < sbs.Count; c++)
                {
                    if (c % 2 == 0)
                    {
                        //Text
                        gross += encode.GetBytes(sbs[c].ToString()).Length;
                    }
                    else
                    {
                        //File
                        using (FileStream fs = new FileStream(sbs[c].ToString(), FileMode.Open, FileAccess.Read))
                        {
                            gross += fs.Length;
                        }
                    }
                }

                //Content length header set
                req.ContentLength = gross;
                using (Stream s = req.GetRequestStream())
                {
                    //post
                    for (int c = 0; c < sbs.Count; c++)
                    {
                        if (c % 2 == 0)
                        {
                            //Text
                            byte[] sendbytes = encode.GetBytes(sbs[c].ToString());
                            s.Write(sendbytes, 0, sendbytes.Length);
                        }
                        else
                        {
                            //File
                            using (FileStream fs = new FileStream(sbs[c].ToString(), FileMode.Open, FileAccess.Read))
                            {
                                byte[] readdata = new byte[0x1000];
                                int readsize = 0;
                                //sending data
                                for (; ; )
                                {
                                    readsize = fs.Read(readdata, 0, readdata.Length);
                                    if (readsize == 0) break;
                                    s.Write(readdata, 0, readsize);
                                }
                            }
                        }
                    }
                }

                //Get response
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                return TreatWebResponse<T>(res, callback, callbackFull);
            }
            catch (Exception e)
            {
                return new OperationResult<T>(req.RequestUri, e);
            }
        }

        /// <summary>
        /// Treat web response
        /// </summary>
        private static OperationResult<T> TreatWebResponse<T>(HttpWebResponse response, DStreamCallback<T> callback, DStreamCallbackFull<T> callbackFull)
        {
            try
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                    case HttpStatusCode.Accepted:
                    case HttpStatusCode.Created:
                    case HttpStatusCode.NoContent:
                    case HttpStatusCode.ResetContent:
                    case HttpStatusCode.PartialContent:
                        if (callbackFull != null)
                        {
                            return new OperationResult<T>(response.ResponseUri, callbackFull(response));
                        }
                        else if (callback != null)
                        {
                            using (Stream sm = response.GetResponseStream())
                                return new OperationResult<T>(response.ResponseUri, callback(sm));
                        }
                        else
                            throw new ArgumentNullException("callback/callbackFull", "param callback or callbackFull must be set.");
                    default:
                        return new OperationResult<T>(response.ResponseUri, false, default(T), GetDescription(response.StatusCode));
                }
            }
            catch (Exception e)
            {
                return new OperationResult<T>(response.ResponseUri, e);
            }
        }

        public struct SendData
        {
            public string Name;
            public string textorfilename;
            public bool filemode;

            /// <summary>
            /// Append text item
            /// </summary>
            /// <param name="n">Field name</param>
            /// <param name="t">Text</param>
            public SendData(string n, string t)
            {
                Name = n;
                textorfilename = t;
                filemode = false;
            }

            /// <summary>
            /// Append some item
            /// </summary>
            /// <param name="n">Field name</param>
            /// <param name="path">file-path/text</param>
            /// <param name="file">set file-mode</param>
            public SendData(string n, string pathort, bool file)
                : this(n, pathort)
            {
                filemode = true;
            }
        }

        /// <summary>
        /// 指定したステータスコードに対する解説を取得します。
        /// </summary>
        /// <param name="status">ステータスコード</param>
        public static string GetDescription(HttpStatusCode status)
        {
            switch (status)
            {
                case HttpStatusCode.BadRequest:
                    return "400 Bad Request";
                case HttpStatusCode.Unauthorized:
                    return "401 UnAuthorized";
                case HttpStatusCode.Forbidden:
                    return "403 Forbidden";
                case HttpStatusCode.NotFound:
                    return "404 Not Found";
                case HttpStatusCode.InternalServerError:
                    return "500 Internal Server Error";
                case HttpStatusCode.BadGateway:
                    return "502 Bad Gateway";
                case HttpStatusCode.ServiceUnavailable:
                    return "503 Service Unavailable";
                default:
                    return ((int)status).ToString() + " " + Enum.GetName(typeof(HttpStatusCode), status);
            }
        }

        /// <summary>
        /// 指定したURIへ接続し、結果をStringで返却します。
        /// </summary>
        /// <param name="uri">接続先URI</param>
        /// <param name="authorize">設定のアカウントを使ってログインするか</param>
        /// <param name="post">POSTメソッドを使うか</param>
        /// <returns></returns>
        public static OperationResult<string> WebConnectDownloadString(
            Uri uri,
            string method,
            ICredentials credential)
        {
            return WebConnect<string>(
                CreateRequest(uri, true),
                method,
                credential,
                new DStreamCallback<string>(CommonStreamReader.ReadStringByStream), null);
        }

        /// <summary>
        /// Frequently used stream-reader
        /// </summary>
        public class CommonStreamReader
        {
            /// <summary>
            /// Get string by stream
            /// </summary>
            public static string ReadStringByStream(Stream strm)
            {
                using (StreamReader sr = new StreamReader(strm))
                {
                    return sr.ReadToEnd();
                }
            }

            public static Image ReadImageByStream(Stream strm)
            {
                return Image.FromStream(strm);
            }
        }
    }
}