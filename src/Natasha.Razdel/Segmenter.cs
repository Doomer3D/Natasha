// source: https://github.com/natasha/razdel/blob/master/razdel/segmenters/base.py

namespace Natasha.Razdel;

/// <summary>
/// сегментатор
/// </summary>
/// <typeparam name="T"> тип разделения </typeparam>
public abstract class Segmenter<T>
    where T : Split
{
    /// <summary>
    /// разделитель
    /// </summary>
    public ISplitter<object>? Splitter { get; set; }

    /// <summary>
    /// пост-обработчик
    /// </summary>
    public Func<string, string>? PostProcessor { get; set; }

    /// <summary>
    /// список правил
    /// </summary>
    public List<Rule<T>> Rules { get; }

    /// <summary>
    /// конструктор
    /// </summary>
    public Segmenter()
    {
        Rules = new List<Rule<T>>();
    }

    /// <summary>
    /// разделить текст
    /// </summary>
    /// <param name="text"> текст </param>
    /// <returns></returns>
    public virtual IEnumerable<Substring> Split(string text)
    {
        return FindSubstrings(SplitAsString(text), text);
    }

    /// <summary>
    /// разделить текст (только строки)
    /// </summary>
    /// <param name="text"> текст </param>
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
    /// сегментировать текст
    /// </summary>
    /// <param name="parts"> токены </param>
    /// <returns></returns>
    protected abstract IEnumerable<string> Segment(IEnumerable<object> parts);

    /// <summary>
    /// проверить, нужно ли объединить разделение
    /// </summary>
    /// <param name="split"> разделение </param>
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
    /// извлечь подстроки из текста
    /// </summary>
    /// <param name="chunks"> чанки </param>
    /// <param name="text"> текст </param>
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
