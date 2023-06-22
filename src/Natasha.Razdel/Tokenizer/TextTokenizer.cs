// source: https://github.com/natasha/razdel/blob/master/razdel/segmenters/tokenize.py

namespace Natasha.Razdel.Tokenizer;

/// <summary>
/// разделитель текста на токены
/// </summary>
public class TextTokenizer : Segmenter<TokenSplit>
{
    /// <summary>
    /// конструктор
    /// </summary>
    public TextTokenizer()
    {
        Splitter = new TokenSplitter();

        Rules.Add(new DashRule());
        Rules.Add(new UnderscoreRule());
        Rules.Add(new FloatRule());
        Rules.Add(new FractionRule());
        Rules.Add(new PunctRule());
        Rules.Add(new OtherRule());
    }

    /// <inheritdoc/>
    protected override IEnumerable<string> Segment(IEnumerable<object> parts)
    {
        using var enumerator = parts.GetEnumerator();
        if (!enumerator.MoveNext()) yield break;

        var buffer = enumerator.Current as string;

        while (enumerator.MoveNext())
        {
            var split = (enumerator.Current as TokenSplit)!;
            var right = enumerator.MoveNext() ? enumerator.Current as string : null;
            split.Buffer = buffer;

            if (split.Delimiter == "" && IsJoin(split))
            {
                buffer += right;
            }
            else
            {
                yield return buffer!;
                buffer = right;
            }
        }

        yield return buffer!;
    }
}
