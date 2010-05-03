using System.Text.RegularExpressions;

namespace Mintw
{
    public static class Define
    {
        public const string Version = "1.1";

        public static Regex URLRegex = new Regex(@"((?:http|https)\:[\w\;\/\?\:\@\&\=\+\$\,\-_\.\!\~\*\'\(\)\%]+)", RegexOptions.Singleline | RegexOptions.Compiled);
    }
}
