// source: https://github.com/natasha/razdel/blob/master/razdel/segmenters/punct.py

namespace Natasha.Razdel;

/// <summary>
/// Пунктуация
/// </summary>
public static partial class Punct
{
    /// <summary>
    /// Знаки окончания предложения
    /// </summary>
    public const string ENDINGS = ".?!…";

    /// <summary>
    /// Дефисы
    /// </summary>
    public const string DASHES = "‑–—−-";

    /// <summary>
    /// Открывающие кавычки
    /// </summary>
    public const string OPEN_QUOTES = "«“‘";

    /// <summary>
    /// Закрывающие кавычки
    /// </summary>
    public const string CLOSE_QUOTES = "»”’";

    /// <summary>
    /// Обычные кавычки
    /// </summary>
    public const string GENERIC_QUOTES = "\"„\'";

    /// <summary>
    /// Все кавычки
    /// </summary>
    public const string QUOTES = OPEN_QUOTES + CLOSE_QUOTES + GENERIC_QUOTES;

    /// <summary>
    /// Открывающие скобки
    /// </summary>
    public const string OPEN_BRACKETS = "([{";

    /// <summary>
    /// Закрывающие скобки
    /// </summary>
    public const string CLOSE_BRACKETS = ")]}";

    /// <summary>
    /// Все скобки
    /// </summary>
    public const string BRACKETS = OPEN_BRACKETS + CLOSE_BRACKETS;

    /// <summary>
    /// Все границы (кавычки и скобки)
    /// </summary>
    public const string BOUNDS = QUOTES + BRACKETS;

    /// <summary>
    /// Смайлики
    /// </summary>
    public const string SMILES = "[=:;]-?[)(]{1,3}";
}
