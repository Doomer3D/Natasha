// source: https://github.com/natasha/razdel/blob/master/razdel/segmenters/tokenize.py

namespace Natasha.Razdel.Tokenizer;

/// <summary>
/// Атом
/// </summary>
/// <param name="start"> Начало </param>
/// <param name="stop"> Конец </param>
/// <param name="type"> Тип </param>
/// <param name="text"> Текст </param>
public class Atom(int start, int stop, AtomType type, string text)
{
    /// <summary>
    /// Начало
    /// </summary>
    public int Start { get; set; } = start;

    /// <summary>
    /// Конец
    /// </summary>
    public int Stop { get; set; } = stop;

    /// <summary>
    /// Тип
    /// </summary>
    public AtomType Type { get; set; } = type;

    /// <summary>
    /// Текст
    /// </summary>
    public string Text { get; set; } = text;

    /// <inheritdoc/>
    public override string ToString() => $"[{Type}] {Text}";
}
