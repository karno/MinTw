using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Std.Tweak.Exceptions
{
    /// <summary>
    /// Twitter xml analyze error
    /// </summary>
    public class TwitterXmlParseException : System.Net.WebException
    {
        public TwitterXmlParseException(string detail) : base("Twitter xml analyzing error:" + detail) { }

        public TwitterXmlParseException(Exception excp) : base("Twitter xml analyzing error:" + excp.Message, excp) { }

        public TwitterXmlParseException(XObject xobj) : base("Twitter xml analyzing error at:" + xobj == null || xobj.Document == null ? "(NULL object)" : xobj.Document.ToString()) { }
    }

    /// <summary>
    /// Twitter api request error
    /// </summary>
    public class TwitterRequestException : System.Net.WebException
    {
        public TwitterRequestException(string detail) : base("Twitter api request error:" + detail) { }

        public TwitterRequestException(Exception excp) : base("Twitter api request error:" + excp.Message, excp) { }

        public TwitterRequestException(XObject xobj) : base("Twitter api request error (XML Error at:" + xobj == null || xobj.Document == null ? "(NULL object)" : xobj.Document.ToString() + ")") { }
    }

    /// <summary>
    /// Twitter oauth authentication error
    /// </summary>
    public class TwitterOAuthRequestException : System.Net.WebException
    {
        public TwitterOAuthRequestException(string detail) : base("Twitter OAuth request error:" + detail) { }
    }
}
