// source: https://github.com/natasha/razdel/blob/master/razdel/segmenters/punct.py

namespace Natasha.Razdel;

/// <summary>
/// пунктуация
/// </summary>
public static partial class Punct
{
    /// <summary>
    /// знаки окончания предложения
    /// </summary>
    public const string ENDINGS = ".?!…";

    /// <summary>
    /// дефисы
    /// </summary>
    public const string DASHES = "‑–—−-";

    /// <summary>
    /// открывающие кавычки
    /// </summary>
    public const string OPEN_QUOTES = "«“‘";

    /// <summary>
    /// закрывающие кавычки
    /// </summary>
    public const string CLOSE_QUOTES = "»”’";

    /// <summary>
    /// обычные кавычки
    /// </summary>
    public const string GENERIC_QUOTES = "\"„\'";

    /// <summary>
    /// все кавычки
    /// </summary>
    public const string QUOTES = OPEN_QUOTES + CLOSE_QUOTES + GENERIC_QUOTES;

    /// <summary>
    /// открывающие скобки
    /// </summary>
    public const string OPEN_BRACKETS = "([{";

    /// <summary>
    /// закрывающие скобки
    /// </summary>
    public const string CLOSE_BRACKETS = ")]}";

    /// <summary>
    /// все скобки
    /// </summary>
    public const string BRACKETS = OPEN_BRACKETS + CLOSE_BRACKETS;

    /// <summary>
    /// все границы (кавычки и скобки)
    /// </summary>
    public const string BOUNDS = QUOTES + BRACKETS;

    /// <summary>
    /// смайлики
    /// </summary>
    public const string SMILES = "[=:;]-?[)(]{1,3}";
}
