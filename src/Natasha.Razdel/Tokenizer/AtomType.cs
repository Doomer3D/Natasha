// source: https://github.com/natasha/razdel/blob/master/razdel/segmenters/tokenize.py

namespace Natasha.Razdel.Tokenizer;

/// <summary>
/// Тип атома
/// </summary>
public enum AtomType
{
    /// <summary>
    /// Прочее
    /// </summary>
    Other = 0,

    /// <summary>
    /// Русское слово
    /// </summary>
    Ru = 1,

    /// <summary>
    /// Латинское слово
    /// </summary>
    Lat = 2,

    /// <summary>
    /// Целое число
    /// </summary>
    Int = 3,

    /// <summary>
    /// Пунктуация
    /// </summary>
    Punct = 4
}
