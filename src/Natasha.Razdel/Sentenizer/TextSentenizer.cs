// source: https://github.com/natasha/razdel/blob/master/razdel/segmenters/sentenize.py

namespace Natasha.Razdel.Sentenizer;

/// <summary>
/// разделитель текста на предложения
/// </summary>
public class TextSentenizer : Segmenter<SentSplit>
{
    /// <summary>
    /// конструктор
    /// </summary>
    public TextSentenizer()
    {
        Splitter = new SentSplitter();
        PostProcessor = e => e.Trim();

        Rules.Add(new EmptySideRule());
        Rules.Add(new NoSpacePrefixRule());
        Rules.Add(new LowerRightRule());
        Rules.Add(new DelimiterRightRule());
        Rules.Add(new SokrLeftRule());
        Rules.Add(new InsidePairSokrRule());
        Rules.Add(new InitialsLeftRule());
        Rules.Add(new ListItemRule());
        Rules.Add(new CloseQuoteRule());
        Rules.Add(new CloseBracketRule());
        Rules.Add(new DashRightRule());
    }

    /// <inheritdoc/>
    protected override IEnumerable<string> Segment(IEnumerable<object> parts)
    {
        using var enumerator = parts.GetEnumerator();
        if (!enumerator.MoveNext()) yield break;

        if (enumerator.Current is not string buffer) yield break;

        while (enumerator.MoveNext())
        {
            var split = (enumerator.Current as SentSplit)!;
            var right = enumerator.MoveNext() ? enumerator.Current as string : null;
            split.Buffer = buffer;

            if (IsJoin(split))
            {
                buffer += split.Delimiter + right;
            }
            else
            {
                yield return buffer! + split.Delimiter;
                buffer = right!;
            }
        }
        yield return buffer;
    }
}
