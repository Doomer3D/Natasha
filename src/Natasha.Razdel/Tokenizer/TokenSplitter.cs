// source: https://github.com/natasha/razdel/blob/master/razdel/segmenters/tokenize.py

using System.Text.RegularExpressions;

namespace Natasha.Razdel.Tokenizer;

/// <summary>
/// Разделитель токенов
/// </summary>
/// <param name="window"> Размер окна </param>
public class TokenSplitter(int window = 3) : Splitter(window)
{
    /// <summary>
    /// Регулярка для разделения строки
    /// </summary>
    private static readonly Regex _splitterRegex;

    /// <summary>
    /// Статический конструктор
    /// </summary>
    static TokenSplitter()
    {
        var punct = RegexHelper.EscapeEx("\\/!#$%&*+,.:;<=>?@^_`|~№…" + Punct.DASHES + Punct.QUOTES + Punct.BRACKETS);
        var pattern = $"""
(?<Ru>[а-яё]+)
|(?<Lat>[a-z]+)
|(?<Int>\d+)
|(?<Punct>[{punct}])
|(?<Other>\S)
""";
        _splitterRegex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
    }

    /// <inheritdoc/>
    public override IEnumerable<object> Split(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) yield break;

        var window = Window;

        var atoms = GetParts(text).ToList();
        for (int i = 0, n = atoms.Count; i < n; i++)
        {
            var atom = atoms[i];
            if (i > 0)
            {
                var previous = atoms[i - 1];
                var delimiter = text[previous.Stop..atom.Start];
                var leftIndex = Math.Max(0, i - window);
                var left = atoms.GetRange(leftIndex, i - leftIndex);
                var right = atoms.GetRange(i, Math.Min(n - i, window));
                yield return new TokenSplit(left, delimiter, right);
            }
            yield return atom.Text;
        }
    }

    /// <summary>
    /// Разделить текст на части
    /// </summary>
    /// <param name="text"> Текст </param>
    /// <returns></returns>
    private IEnumerable<Atom> GetParts(string text)
    {
        foreach (var match in _splitterRegex.Matches(text).Cast<Match>())
        {
            var type = match.Groups.Cast<Group>().Skip(1).FirstOrDefault(e => e.Success && e.Name != null)?.Name;
            yield return new Atom(
                match.Index,
                match.Index + match.Length,
                Enum.Parse<AtomType>(type!),
                match.Value
            );
        }
    }
}
