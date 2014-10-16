
namespace Wonderland.Logic.Extensions
{
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static string StripHtml(this string htmlString)
        {
            return Regex.Replace(htmlString, @"<(.|\n)*?>", string.Empty);

        }
    }
}
