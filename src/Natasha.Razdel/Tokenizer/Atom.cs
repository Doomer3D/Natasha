// source: https://github.com/natasha/razdel/blob/master/razdel/segmenters/tokenize.py

namespace Natasha.Razdel.Tokenizer;

/// <summary>
/// атом
/// </summary>
public class Atom
{
    /// <summary>
    /// начало
    /// </summary>
    public int Start { get; set; }

    /// <summary>
    /// конец
    /// </summary>
    public int Stop { get; set; }

    /// <summary>
    /// тип
    /// </summary>
    public AtomType Type { get; set; }

    /// <summary>
    /// текст
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// конструктор
    /// </summary>
    /// <param name="start"> начало </param>
    /// <param name="stop"> конец </param>
    /// <param name="type"> тип </param>
    /// <param name="text"> текст </param>
    public Atom(int start, int stop, AtomType type, string text)
    {
        Start = start;
        Stop = stop;
        Type = type;
        Text = text;
    }

    public override string ToString() => $"[{Type}] {Text}";
}
