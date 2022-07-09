using System;

namespace EasyCommerce;

public static class StringExtensions
{
    public static bool IsNullOrWhiteSpace(this string str) => string.IsNullOrWhiteSpace(str);
    public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);
    public static bool IsNotNullOrEmpty(this string str) => !str.IsNullOrEmpty();
    public static bool IsNotNullOrWhiteSpace(this string str) => !str.IsNotNullOrWhiteSpace();
    public static bool IsWhiteSpaceOrEmpty(this string str)
    {
        if (str.IsNullOrWhiteSpace())
        {
            return true;
        }
        else if (str.IsNullOrEmpty())
        {
            return true;
        }
        return false;
    }
    public static void ThrowIfIsNullOrWhiteSpaceOrEmpty(this string str, string propertyName)
    {
        if (str.IsNullOrEmpty())
        {
            throw new ArgumentNullException(str, $"Property `{propertyName}` cannot be empty.");
        }
        else if (str.IsNullOrWhiteSpace())
        {
            throw new ArgumentNullException(str, $"Property `{propertyName}` cannot be whitespace or null.");
        }
    }
}
