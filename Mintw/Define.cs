using System.Text.RegularExpressions;

namespace Mintw
{
    public static class Define
    {
        public const string Version = "1.0";

        public static Regex URLRegex = new Regex(@"((?:http|https)\:[\w\;\/\?\:\@\&\=\+\$\,\-_\.\!\~\*\'\(\)\%]+)", RegexOptions.Singleline | RegexOptions.Compiled);
    }
}
