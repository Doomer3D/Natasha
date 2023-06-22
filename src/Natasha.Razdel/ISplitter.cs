namespace Natasha.Razdel;

/// <summary>
/// интерфейс разделителя
/// </summary>
/// <typeparam name="T"> тип элемента </typeparam>
public interface ISplitter<T>
{
    /// <summary>
    /// разделить текст
    /// </summary>
    /// <param name="text"> текст </param>
    /// <returns></returns>
    IEnumerable<T> Split(string text);
}
