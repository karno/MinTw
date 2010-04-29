using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Std.Network;
using System.Net;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace Std.Tweak.CredentialProviders
{
    /// <summary>
    /// Basic credential
    /// </summary>
    /// <remarks>
    /// We STRONGLY recommended to use OAuth (or XAuth) alternate this.
    /// </remarks>
    [Obsolete("Basic authentication will unable by 2010/6.")]
    public class Basic : CredentialProvider
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Basic() : this(null, null) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userName">username</param>
        /// <param name="password">password</param>
        public Basic(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }

        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Pass word
        /// </summary>
        public string Password { get; set; }

        protected virtual string TwitterUri { get { return "http://api.twitter.com/1/"; } }

        public sealed override System.Xml.Linq.XDocument RequestAPI(string uriParticle, CredentialProvider.RequestMethod method, IEnumerable<KeyValuePair<string, string>> param)
        {
            if (String.IsNullOrEmpty(uriParticle))
                throw new ArgumentNullException(uriParticle);
            else if (uriParticle.Length < 5)
                throw new ArgumentException("uri is too short.");
            string target = TwitterUri + (uriParticle.EndsWith("/") ? uriParticle.Substring(1) : uriParticle);

            if (target.EndsWith("format"))
                target = target.Substring(0, target.Length - 6) + "xml";
            else if (target.EndsWith("json"))
                target = target.Substring(0, target.Length - 4) + "xml";

            try
            {
                var req = Http.CreateRequest(new Uri(target), true);
                req.Credentials = new System.Net.NetworkCredential(UserName, Password);
                var ret = Http.WebConnect<XDocument>(
                    req,
                    method.ToString(), null,
                    new Http.DStreamCallbackFull<XDocument>((res) =>
                    {
                        int rateLimit;
                        if (int.TryParse(res.Headers["X-RateLimit-Limit"], out rateLimit))
                        {
                            this.RateLimitMax = rateLimit;
                        }
                        int rateLimitRemaining;
                        if (int.TryParse(res.Headers["X-RateLimit-Remaining"], out rateLimitRemaining))
                        {
                            this.RateLimitRemaining = rateLimitRemaining;
                        }
                        long rateLimitReset;
                        if (long.TryParse(res.Headers["X-RateLimit-Reset"], out rateLimitReset))
                        {
                            this.RateLimitReset = UnixEpoch.GetDateTimeByUnixEpoch(rateLimitReset);
                        }

                        XDocument xd = null;
                        try
                        {
                            using (var s = res.GetResponseStream())
                            {
                                using (var sr = new StreamReader(s))
                                {
                                    xd = XDocument.Load(sr);
                                }
                            }
                        }
                        catch (XmlException)
                        {
                            throw;
                        }
                        return xd;
                    }));
                if (ret.Succeeded && ret.Data != null)
                {
                    return ret.Data;
                }
                else
                {
                    if (ret.Exception != null)
                        throw ret.Exception;
                    else
                        throw new WebException(ret.Message);
                }
            }
            catch (WebException we)
            {
                System.Diagnostics.Debug.WriteLine(we.ToString());
            }
            catch (XmlException xe)
            {
                throw new Exceptions.TwitterXmlParseException(xe);
            }
            catch (IOException)
            {
                throw;
            }

            return null;
        }
    }
}