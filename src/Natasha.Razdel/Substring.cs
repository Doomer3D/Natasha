namespace Natasha.Razdel;

/// <summary>
/// подстрока
/// </summary>
public class Substring
{
    /// <summary>
    /// начало
    /// </summary>
    public int Start { get; init; }

    /// <summary>
    /// конец
    /// </summary>
    public int Stop { get; init; }

    /// <summary>
    /// текст
    /// </summary>
    public string Text { get; init; }

    /// <summary>
    /// конструктор
    /// </summary>
    /// <param name="start"> начало </param>
    /// <param name="stop"> конец </param>
    /// <param name="text"> текст </param>
    public Substring(int start, int stop, string text) =>
        (Start, Stop, Text) = (start, stop, text);

    /// <summary>
    /// деконструктор
    /// </summary>
    /// <param name="start"> начало </param>
    /// <param name="stop"> конец </param>
    /// <param name="text"> текст </param>
    public void Deconstruct(out int start, out int stop, out string text) =>
        (start, stop, text) = (Start, Stop, Text);

    public override string ToString() => $"[{Start}:{Stop}] {Text}";
}
