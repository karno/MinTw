using System;
using System.Xml.Linq;
using Std.Network;

namespace Mintw
{
    public class JmpUrlCompress
    {
        public string Target
        {
            //ID:{0} Key:{1}
            get { return "http://api.j.mp/shorten?version=2.0.1&login={0}&apiKey={1}&format=xml&longUrl="; }
        }

        public string CheckStart
        {
            get { return "http://j.mp"; }
        }

        public bool IsCompressed(string url)
        {
            return url.StartsWith(CheckStart);
        }

        public bool TryCompress(string url, out string compressed)
        {
            if (url.StartsWith(CheckStart))
            {
                //already compressed
                compressed = url;
                return true;
            }
            compressed = null;
            if (String.IsNullOrEmpty(Kernel.Config.JmpId) ||
                String.IsNullOrEmpty(Kernel.Config.JmpPass) ||
                String.IsNullOrEmpty(url))
            {
                return false;
            }
            var escaped = Uri.EscapeDataString(url);
            var target = String.Format(Target,
                Kernel.Config.JmpId.Replace(" ", ""),
                Kernel.Config.JmpPass.Value.Replace(" ", "")) + escaped;
            var ret = Http.WebConnect<string>(
                Http.CreateRequest(new Uri(target), true), "GET",
                null, ((Http.DStreamCallback<string>)((u) =>
                {
                    var elem = XElement.Load(System.Xml.XmlReader.Create(u));
                    if ((int)elem.Element("errorCode") == 0)
                        return (elem.Element("results").Element("nodeKeyVal").Element("shortUrl")).Value;
                    else
                        return null;
                })));
            if (ret.Succeeded && !String.IsNullOrEmpty(ret.Data))
            {
                compressed = ret.Data;
                return true;
            }
            else
                return false;
        }

        public bool TryDecompress(string url, out string decompressed)
        {
            decompressed = null;
            return false;
        }
    }
}
