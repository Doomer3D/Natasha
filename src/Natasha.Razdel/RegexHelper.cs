using System.Text.RegularExpressions;

namespace Natasha.Razdel;

/// <summary>
/// помощник для работы с регулярками
/// </summary>
public static class RegexHelper
{
    /// <summary>
    /// расширенное экранирование символов шаблона регулярки
    /// </summary>
    /// <param name="text"> текст </param>
    /// <returns></returns>
    public static string EscapeEx(string text)
    {
        return Regex.Escape(text)
            .Replace("-", @"\-")
            .Replace("]", @"\]");
    }
}
