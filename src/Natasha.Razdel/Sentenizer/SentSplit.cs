// source: https://github.com/natasha/razdel/blob/master/razdel/segmenters/sentenize.py

using System.Text.RegularExpressions;

namespace Natasha.Razdel.Sentenizer;

/// <summary>
/// разделение предложений
/// </summary>
public partial class SentSplit : Split
{
    /// <summary>
    /// признак, что элемент слева - пробел
    /// </summary>
    public bool LeftSpaceSuffix => _leftSpaceSuffix.Value;

    /// <summary>
    /// признак, что элемент справа - пробел
    /// </summary>
    public bool RightSpacePrefix => _rightSpacePrefix.Value;

    /// <summary>
    /// токен слева
    /// </summary>
    public string? LeftToken => _leftToken.Value;

    /// <summary>
    /// токен справа
    /// </summary>
    public string? RightToken => _rightToken.Value;

    /// <summary>
    /// парное сокращение слева
    /// </summary>
    public Match LeftPairSokr => _leftPairSokr.Value;

    /// <summary>
    /// токены из буфера
    /// </summary>
    public string[] BufferTokens => _bufferTokens.Value;

    /// <summary>
    /// слово справа
    /// </summary>
    public string? RightWord => _rightWord.Value;

    private readonly Lazy<bool> _leftSpaceSuffix;
    private readonly Lazy<bool> _rightSpacePrefix;
    private readonly Lazy<string?> _leftToken;
    private readonly Lazy<string?> _rightToken;
    private readonly Lazy<Match> _leftPairSokr;
    private readonly Lazy<string[]> _bufferTokens;
    private readonly Lazy<string?> _rightWord;

    //@cached_property
    //def left_int_sokr(self):
    //    match = INT_SOKR.search(self.left)
    //    if match:
    //        return match.group(1)

    //@cached_property
    //def buffer_first_token(self):
    //    match = FIRST_TOKEN.match(self.buffer)
    //    if match:
    //        return match.group(1)

    /// <summary>
    /// конструктор
    /// </summary>
    public SentSplit(string left, string delimiter, string right)
    {
        _leftSpaceSuffix = new(() =>
        {
            return _spaceSuffix.IsMatch(Left!);
        });
        _rightSpacePrefix = new(() =>
        {
            return _spacePrefix.IsMatch(Right!);
        });
        _leftToken = new(() =>
        {
            var m = _lastToken.Match(left);
            if (m.Success)
                return m.Groups[1].Value;
            else
                return null;
        });
        _rightToken = new(() =>
        {
            var m = _firstToken.Match(right);
            if (m.Success)
                return m.Groups[1].Value;
            else
                return null;
        });
        _leftPairSokr = new(() =>
        {
            return _pairSokr.Match(left);
        });
        _bufferTokens = new(() =>
        {
            if (string.IsNullOrWhiteSpace(Buffer)) return Array.Empty<string>();
            return _token.Matches(Buffer).Select(e => e.Value).ToArray();
        });
        _rightWord = new(() =>
        {
            var m = _word.Match(right);
            return m.Success ? m.Groups[1].Value : null;
        });

        Left = left;
        Delimiter = delimiter;
        Right = right;
    }

    private static readonly Regex _spacePrefix = SpacePrefixRegex();
    private static readonly Regex _spaceSuffix = SpaceSuffixRegex();
    private static readonly Regex _firstToken = FirstTokenRegex();
    private static readonly Regex _lastToken = LastTokenRegex();
    private static readonly Regex _pairSokr = PairSokrRegex();
    private static readonly Regex _token = TokenRegex();
    private static readonly Regex _word = WordRegex();

    [GeneratedRegex(@"^\s", RegexOptions.CultureInvariant)]
    private static partial Regex SpacePrefixRegex();

    [GeneratedRegex(@"\s$", RegexOptions.CultureInvariant)]
    private static partial Regex SpaceSuffixRegex();

    [GeneratedRegex(@"^\s*([^\W\d]+|\d+|[^\w\s])", RegexOptions.CultureInvariant)]
    private static partial Regex FirstTokenRegex();

    [GeneratedRegex(@"([^\W\d]+|\d+|[^\w\s])\s*$", RegexOptions.CultureInvariant)]
    private static partial Regex LastTokenRegex();

    [GeneratedRegex(@"(\w)\s*\.\s*(\w)\s*$", RegexOptions.CultureInvariant)]
    private static partial Regex PairSokrRegex();

    [GeneratedRegex(@"([^\W\d]+|\d+|[^\w\s])", RegexOptions.CultureInvariant)]
    private static partial Regex TokenRegex();

    [GeneratedRegex(@"([^\W\d]+|\d+)", RegexOptions.CultureInvariant)]
    private static partial Regex WordRegex();
}
