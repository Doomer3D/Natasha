namespace Natasha.Razdel;

/// <summary>
/// разделитель
/// </summary>
public abstract class Splitter : ISplitter<object>
{
    /// <summary>
    /// размер окна
    /// </summary>
    public int Window { get; set; }

    /// <summary>
    /// конструктор
    /// </summary>
    /// <param name="window"> размер окна </param>
    public Splitter(int window)
    {
        Window = window;
    }

    /// <summary>
    /// разделить текст
    /// </summary>
    /// <param name="text"> текст </param>
    /// <returns></returns>
    public abstract IEnumerable<object> Split(string text);
}
