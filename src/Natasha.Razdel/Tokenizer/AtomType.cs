// source: https://github.com/natasha/razdel/blob/master/razdel/segmenters/tokenize.py

namespace Natasha.Razdel.Tokenizer;

/// <summary>
/// тип атома
/// </summary>
public enum AtomType
{
    /// <summary>
    /// прочее
    /// </summary>
    Other = 0,

    /// <summary>
    /// русское слово
    /// </summary>
    Ru = 1,

    /// <summary>
    /// латинское слово
    /// </summary>
    Lat = 2,

    /// <summary>
    /// целое число
    /// </summary>
    Int = 3,

    /// <summary>
    /// пунктуация
    /// </summary>
    Punct = 4
}
