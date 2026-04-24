namespace Natasha.Razdel;

/// <summary>
/// Интерфейс разделителя
/// </summary>
/// <typeparam name="T"> Тип элемента </typeparam>
public interface ISplitter<T>
{
    /// <summary>
    /// Разделить текст
    /// </summary>
    /// <param name="text"> Текст </param>
    /// <returns></returns>
    IEnumerable<T> Split(string text);
}
