// source: https://github.com/natasha/razdel/blob/master/razdel/segmenters/base.py

namespace Natasha.Razdel;

/// <summary>
/// Сегментатор
/// </summary>
/// <typeparam name="T"> Тип разделения </typeparam>
public abstract class Segmenter<T>
    where T : Split
{
    /// <summary>
    /// Разделитель
    /// </summary>
    public ISplitter<object>? Splitter { get; set; }

    /// <summary>
    /// Пост-обработчик
    /// </summary>
    public Func<string, string>? PostProcessor { get; set; }

    /// <summary>
    /// Список правил
    /// </summary>
    public List<Rule<T>> Rules { get; }

    /// <summary>
    /// Конструктор
    /// </summary>
    public Segmenter()
    {
        Rules = [];
    }

    /// <summary>
    /// Разделить текст
    /// </summary>
    /// <param name="text"> Текст </param>
    /// <returns></returns>
    public virtual IEnumerable<Substring> Split(string text)
    {
        return FindSubstrings(SplitAsString(text), text);
    }

    /// <summary>
    /// Разделить текст (только строки)
    /// </summary>
    /// <param name="text"> Текст </param>
    /// <returns></returns>
    public virtual IEnumerable<string> SplitAsString(string text)
    {
        var parts = Splitter!.Split(text);
        var chunks = Segment(parts);
        var post = PostProcessor;
        if (post == null)
        {
            foreach (var chunk in chunks)
            {
                yield return chunk;
            }
        }
        else
        {
            foreach (var chunk in chunks)
            {
                yield return post(chunk);
            }
        }
    }

    /// <summary>
    /// Сегментировать текст
    /// </summary>
    /// <param name="parts"> Токены </param>
    /// <returns></returns>
    protected abstract IEnumerable<string> Segment(IEnumerable<object> parts);

    /// <summary>
    /// Проверить, нужно ли объединить разделение
    /// </summary>
    /// <param name="split"> Разделение </param>
    /// <returns></returns>
    protected virtual bool IsJoin(T split)
    {
        foreach (var rule in Rules)
        {
            var action = rule.Check(split);
            if (action != RuleAction.None)
            {
                return action == RuleAction.Join;
            }
        }
        return false;
    }

    /// <summary>
    /// Извлечь подстроки из текста
    /// </summary>
    /// <param name="chunks"> Чанки </param>
    /// <param name="text"> Текст </param>
    /// <returns></returns>
    private IEnumerable<Substring> FindSubstrings(IEnumerable<string> chunks, string text)
    {
        var offset = 0;
        foreach (var chunk in chunks)
        {
            var start = text.IndexOf(chunk, offset);
            var stop = start + chunk.Length;
            yield return new Substring(start, stop, chunk);
            offset = stop;
        }
    }
}
