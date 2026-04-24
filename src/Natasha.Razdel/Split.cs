// source: https://github.com/natasha/razdel/blob/master/razdel/split.py

namespace Natasha.Razdel;

/// <summary>
/// Разделение
/// </summary>
public class Split
{
    /// <summary>
    /// Левая часть
    /// </summary>
    public string? Left { get; init; }

    /// <summary>
    /// Разделитель
    /// </summary>
    public string? Delimiter { get; init; }

    /// <summary>
    /// Правая часть
    /// </summary>
    public string? Right { get; init; }

    /// <summary>
    /// Буфер
    /// </summary>
    public string? Buffer { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    public Split() { }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="left"> Левая часть </param>
    /// <param name="delimiter"> Разделитель </param>
    /// <param name="right"> Правая часть </param>
    /// <param name="buffer"> Буфер </param>
    public Split(string left, string delimiter, string right, string? buffer = null)
    {
        Left = left;
        Delimiter = delimiter;
        Right = right;
        Buffer = buffer;
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        if (string.IsNullOrEmpty(Delimiter))
            return $"{Left} | {Right}";
        else if (string.IsNullOrWhiteSpace(Delimiter))
            return $"{Left} | | {Right}";
        else
            return $"{Left} | {Delimiter} | {Right}";
    }
}
