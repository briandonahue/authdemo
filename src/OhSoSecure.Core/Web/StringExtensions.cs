using System.Globalization;
using System.Text.RegularExpressions;

namespace OhSoSecure.Core.Web
{
	public static class StringExtensions
	{
		public static string ToSeparatedWords(this string value)
		{
		    return value != null ? Regex.Replace(value, "([A-Z0-9][a-z]?)", " $1").Trim() : null;
		}

	    public static string ToInvariantString(this int  value) {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToInvariantString(this bool  value) {
            return value.ToString(CultureInfo.InvariantCulture);
        }

	}
}