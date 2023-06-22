// source: https://github.com/natasha/razdel/blob/master/razdel/split.py

namespace Natasha.Razdel;

/// <summary>
/// разделение
/// </summary>
public class Split
{
    /// <summary>
    /// левая часть
    /// </summary>
    public string? Left { get; init; }

    /// <summary>
    /// разделитель
    /// </summary>
    public string? Delimiter { get; init; }

    /// <summary>
    /// правая часть
    /// </summary>
    public string? Right { get; init; }

    /// <summary>
    /// буфер
    /// </summary>
    public string? Buffer { get; set; }

    /// <summary>
    /// конструктор
    /// </summary>
    public Split() { }

    /// <summary>
    /// конструктор
    /// </summary>
    /// <param name="left"> левая часть </param>
    /// <param name="delimiter"> разделитель </param>
    /// <param name="right"> правая часть </param>
    /// <param name="buffer"> буфер </param>
    public Split(string left, string delimiter, string right, string? buffer = null)
    {
        Left = left;
        Delimiter = delimiter;
        Right = right;
        Buffer = buffer;
    }

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
