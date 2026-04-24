namespace Natasha.Razdel;

/// <summary>
/// Разделитель
/// </summary>
/// <param name="window"> Размер окна </param>
public abstract class Splitter(int window) : ISplitter<object>
{
    /// <summary>
    /// Размер окна
    /// </summary>
    public int Window { get; set; } = window;

    /// <summary>
    /// Разделить текст
    /// </summary>
    /// <param name="text"> Текст </param>
    /// <returns></returns>
    public abstract IEnumerable<object> Split(string text);
}
