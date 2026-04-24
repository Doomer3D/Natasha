// source: https://github.com/natasha/razdel/blob/master/razdel/segmenters/sentenize.py

using System.Text.RegularExpressions;

namespace Natasha.Razdel.Sentenizer;

/// <summary>
/// Разделитель предложений
/// </summary>
/// <param name="window"> Размер окна </param>
public class SentSplitter(int window = 10) : Splitter(window)
{
    /// <summary>
    /// Регулярка для разделения строки
    /// </summary>
    private static readonly Regex _splitterRegex;

    /// <summary>
    /// Статический конструктор
    /// </summary>
    static SentSplitter()
    {
        var smiles = Punct.SMILES;
        var delimiters = RegexHelper.EscapeEx(Punct.ENDINGS + ";" + Punct.GENERIC_QUOTES + Punct.CLOSE_QUOTES + Punct.CLOSE_BRACKETS);
        var pattern = $"({smiles}|[{delimiters}])";
        _splitterRegex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
    }

    /// <inheritdoc/>
    public override IEnumerable<object> Split(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) yield break;

        var window = Window;
        var len = text.Length;

        int previous = 0, start, stop;
        foreach (var match in _splitterRegex.Matches(text).Cast<Match>())
        {
            start = match.Index;
            stop = match.Index + match.Length;
            var delimiter = match.Groups[1].Value;
            yield return text[previous..start];
            var left = text[Math.Max(0, start - window)..start];
            var right = text[stop..Math.Min(stop + window, len)];
            yield return new SentSplit(left, delimiter, right);
            previous = stop;
        }
        yield return text[previous..];
    }
}
