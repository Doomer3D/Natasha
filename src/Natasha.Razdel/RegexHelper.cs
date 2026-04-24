using System.Text.RegularExpressions;

namespace Natasha.Razdel;

/// <summary>
/// Помощник для работы с регулярками
/// </summary>
public static class RegexHelper
{
    /// <summary>
    /// Расширенное экранирование символов шаблона регулярки
    /// </summary>
    /// <param name="text"> Текст </param>
    /// <returns></returns>
    public static string EscapeEx(string text)
    {
        return Regex.Escape(text)
            .Replace("-", @"\-")
            .Replace("]", @"\]");
    }
}
