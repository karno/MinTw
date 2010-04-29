using System;
using System.Drawing;
using System.Xml.Linq;

namespace Std.Tweak
{
    /// <summary>
    /// XML node parsers
    /// </summary>
    /// X/O Mapping?w
    internal static class Parsers
    {
        internal static string ParseString(this XElement e)
        {
            return e == null ? null : e.Value.Replace("&lt;", "<").Replace("&gt;", ">");
        }

        internal static bool ParseBool(this XElement e)
        {
            return ParseBool(e, false);
        }

        internal static bool ParseBool(this XElement e, bool def)
        {
            return ParseBool(e == null ? null : e.Value, def);
        }

        internal static bool ParseBool(this string s, bool def)
        {
            if (s == null)
            {
                return def;
            }
            return def ? s.ToLower() != "false" : s.ToLower() == "true";
        }

        internal static long ParseLong(this XElement e)
        {
            return ParseLong(e == null ? null : e.Value);
        }

        internal static long ParseLong(string s)
        {
            long v;
            return long.TryParse(s, out v) ? v : 0;
        }

        internal static DateTime ParseDateTime(this XElement e)
        {
            return ParseDateTime(e == null ? null : e.Value);
        }

        internal static DateTime ParseDateTime(this string s)
        {
            return DateTime.Parse(s);
        }

        internal static DateTime ParseDateTime(this XElement e, string format)
        {
            return ParseDateTime(e == null ? null : e.Value, format);
        }

        internal static DateTime ParseDateTime(this string s, string format)
        {
            return DateTime.ParseExact(s,
                format,
                System.Globalization.DateTimeFormatInfo.InvariantInfo,
                System.Globalization.DateTimeStyles.None);
        }

        internal static TimeSpan ParseUtcOffset(this XElement e)
        {
            return ParseUtcOffset(e == null ? null : e.Value);
        }

        internal static TimeSpan ParseUtcOffset(this string s)
        {
            int seconds;
            int.TryParse(s, out seconds);
            return new TimeSpan(0, 0, seconds);
        }

        internal static Color ParseColor(this XElement e)
        {
            return ParseColor(e == null ? null : e.Value);
        }

        internal static Color ParseColor(this string s)
        {
            if (s == null || s.Length != 6)
            {
                return Color.Transparent;
            }

            int v, r, g, b;
            v = Convert.ToInt32(s, 16);
            r = v >> 16;
            g = (v >> 8) & 0xFF;
            b = v & 0xFF;
            return Color.FromArgb(r, g, b);
        }

        internal static Uri ParseUri(this XElement e)
        {
            var uri = e.ParseString();
            try
            {
                if (String.IsNullOrEmpty(uri))
                    return null;
                else
                    return new Uri(uri);
            }
            catch (UriFormatException)
            {
                return null;
            }
        }
    }
}
