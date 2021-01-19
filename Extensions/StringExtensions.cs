namespace Orbital.Core
{
    public static class StringExtensions
    {
        public static string ToLowerFirstLetter(this string text) { return char.ToLowerInvariant(text[0]) + text.Substring(1); }
    }
}