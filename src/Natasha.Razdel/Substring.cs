namespace Natasha.Razdel;

/// <summary>
/// Подстрока
/// </summary>
public class Substring
{
    /// <summary>
    /// Начало
    /// </summary>
    public int Start { get; init; }

    /// <summary>
    /// Конец
    /// </summary>
    public int Stop { get; init; }

    /// <summary>
    /// Текст
    /// </summary>
    public string Text { get; init; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="start"> Начало </param>
    /// <param name="stop"> Конец </param>
    /// <param name="text"> Текст </param>
    public Substring(int start, int stop, string text) =>
        (Start, Stop, Text) = (start, stop, text);

    /// <summary>
    /// Деконструктор
    /// </summary>
    /// <param name="start"> Начало </param>
    /// <param name="stop"> Конец </param>
    /// <param name="text"> Текст </param>
    public void Deconstruct(out int start, out int stop, out string text) =>
        (start, stop, text) = (Start, Stop, Text);

    /// <inheritdoc/>
    public override string ToString() => $"[{Start}:{Stop}] {Text}";
}
