// source: https://github.com/natasha/razdel/blob/master/razdel/segmenters/tokenize.py

using System.Text.RegularExpressions;

namespace Natasha.Razdel.Tokenizer;

/// <summary>
/// правило AB|BA
/// </summary>
public abstract class RuleABBA : Rule<TokenSplit>
{
    public override RuleAction Check(TokenSplit split)
    {
        Atom? left, right;
        if (IsDelimiter(split.Left!))
        {
            // что-|то
            (left, right) = (split.Left2, split.Right1);
        }
        else if (IsDelimiter(split.Right!))
        {
            // что|-то
            (left, right) = (split.Left1, split.Right2);
        }
        else return RuleAction.None;

        if (left == null || right == null) return RuleAction.None;

        return InnerCheck(left, right);
    }

    /// <summary>
    /// проверить, что токен является разделителем
    /// </summary>
    /// <param name="delimiter"> токен </param>
    /// <returns></returns>
    public abstract bool IsDelimiter(string delimiter);

    /// <summary>
    /// внутренняя проверка правила
    /// </summary>
    /// <param name="left"> атом слева </param>
    /// <param name="right"> атом справа </param>
    /// <returns></returns>
    protected abstract RuleAction InnerCheck(Atom left, Atom right);
}

/// <summary>
/// правило "тире":
/// когда-нибудь | 100-200
/// </summary>
public class DashRule : RuleABBA
{
    public override bool IsDelimiter(string delimiter) =>
        Punct.DASHES.Contains(delimiter);

    protected override RuleAction InnerCheck(Atom left, Atom right) =>
        left.Type == AtomType.Punct || right.Type == AtomType.Punct
            ? RuleAction.None : RuleAction.Join;
}

/// <summary>
/// правило "подчеркивание":
/// настоящим_программистам_пробел_не_нужен
/// </summary>
public class UnderscoreRule : RuleABBA
{
    public override bool IsDelimiter(string delimiter) =>
        delimiter == "_";

    protected override RuleAction InnerCheck(Atom left, Atom right) =>
        left.Type == AtomType.Punct || right.Type == AtomType.Punct
            ? RuleAction.None : RuleAction.Join;
}

/// <summary>
/// правило "вещественное число"
/// </summary>
public class FloatRule : RuleABBA
{
    public override bool IsDelimiter(string delimiter) =>
        ".,".Contains(delimiter);

    protected override RuleAction InnerCheck(Atom left, Atom right) =>
        left.Type == AtomType.Int && right.Type == AtomType.Int
            ? RuleAction.Join : RuleAction.None;
}

/// <summary>
/// правило "дробь"
/// </summary>
public class FractionRule : RuleABBA
{
    public override bool IsDelimiter(string delimiter) =>
        @"/\".Contains(delimiter);

    protected override RuleAction InnerCheck(Atom left, Atom right) =>
        left.Type == AtomType.Int && right.Type == AtomType.Int
            ? RuleAction.Join : RuleAction.None;
}

/// <summary>
/// правило "пунктуация":
/// !? | ...
/// </summary>
public partial class PunctRule : Rule<TokenSplit>
{
    private static readonly Regex smile = SmileRegex();

    public override RuleAction Check(TokenSplit split)
    {
        if (split.Left1.Type != AtomType.Punct || split.Right1.Type != AtomType.Punct) return RuleAction.None;

        var left = split.Left!;
        var right = split.Right!;

        if (smile.IsMatch(split.Buffer + right))
            return RuleAction.Join;

        if (Punct.ENDINGS.Contains(left) && Punct.ENDINGS.Contains(right))
            // ... | ?!
            return RuleAction.Join;

        if (left + right == "--" || left + right == "**")
            // ***
            return RuleAction.Join;

        return RuleAction.None;
    }

    /// <summary>
    /// регулярка смайликов
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex("^" + Punct.SMILES + "$", RegexOptions.CultureInvariant)]
    private static partial Regex SmileRegex();
}

/// <summary>
/// правило "прочее":
/// ΔP | Δσ mβж
/// </summary>
public class OtherRule : Rule<TokenSplit>
{
    public override RuleAction Check(TokenSplit split)
    {
        var left = split.Left1.Type;
        var right = split.Right1.Type;

        if (left == AtomType.Other && (
            right == AtomType.Other ||
            right == AtomType.Ru ||
            right == AtomType.Lat))
            // ΔP
            return RuleAction.Join;

        if (right == AtomType.Other && (
            left == AtomType.Other ||
            left == AtomType.Ru ||
            left == AtomType.Lat))
            // Δσ mβж
            return RuleAction.Join;

        return RuleAction.None;
    }
}
