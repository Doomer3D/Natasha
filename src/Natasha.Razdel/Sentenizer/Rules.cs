// source: https://github.com/natasha/razdel/blob/master/razdel/segmenters/sentenize.py

using System.Text.RegularExpressions;

namespace Natasha.Razdel.Sentenizer;

/// <summary>
/// Правило "хз":
/// </summary>
/// <remarks>
/// Не срабатывает никогда =)
/// </remarks>
public class EmptySideRule : Rule<SentSplit>
{
    /// <inheritdoc/>
    public override RuleAction Check(SentSplit split)
    {
        if (split.LeftToken == null || split.RightToken == null)
            return RuleAction.Join;

        return RuleAction.None;
    }
}

/// <summary>
/// Правило "справа от разделителя нет пробела":
/// </summary>
/// <remarks>
/// "Так в чем же дело? | "
/// </remarks>
public class NoSpacePrefixRule : Rule<SentSplit>
{
    public override RuleAction Check(SentSplit split)
    {
        if (!split.RightSpacePrefix)
            return RuleAction.Join;

        return RuleAction.None;
    }
}

/// <summary>
/// Правило "текст справа с маленькой буквы":
/// </summary>
/// <remarks>
/// Сталин+одобряет
/// </remarks>
public class LowerRightRule : Rule<SentSplit>
{
    /// <inheritdoc/>
    public override RuleAction Check(SentSplit split)
    {
        if (IsLowerWord(split.RightToken))
            return RuleAction.Join;

        return RuleAction.None;
    }

    /// <summary>
    /// Проверить, что токен является словом в нижнем регистре
    /// </summary>
    /// <param name="token"> токен </param>
    /// <returns></returns>
    private bool IsLowerWord(string? token)
    {
        if (string.IsNullOrEmpty(token)) return false;
        return token.All(e => char.IsLetter(e) && char.IsLower(e));
    }
}

/// <summary>
/// Правило "разделитель справа"
/// </summary>
public partial class DelimiterRightRule : Rule<SentSplit>
{
    private const string DELIMITERS = Punct.ENDINGS + ";" + Punct.GENERIC_QUOTES + Punct.CLOSE_QUOTES + Punct.CLOSE_BRACKETS;

    private static readonly Regex smile = SmileRegex();

    /// <inheritdoc/>
    public override RuleAction Check(SentSplit split)
    {
        var right = split.RightToken!;

        if (Punct.GENERIC_QUOTES.Contains(right))
            return RuleAction.None;

        if (DELIMITERS.Contains(right))
            return RuleAction.Join;

        if (smile.IsMatch(split.Right!))
            return RuleAction.Join;

        return RuleAction.None;
    }

    /// <summary>
    /// Регулярка смайликов
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(@"^\s*" + Punct.SMILES, RegexOptions.CultureInvariant)]
    private static partial Regex SmileRegex();
}

/// <summary>
/// Правило "сокращение слева":
/// </summary>
/// <remarks>
/// И т.д. | тов. Сталин
/// </remarks>
public class SokrLeftRule : Rule<SentSplit>
{
    /// <inheritdoc/>
    public override RuleAction Check(SentSplit split)
    {
        if (split.Delimiter != ".")
            return RuleAction.None;

        var right = split.RightToken!;
        var match = split.LeftPairSokr;

        if (match.Success)
        {
            var item = (match.Groups[1].Value.ToLower(), match.Groups[2].Value.ToLower());
            if (Sokr.HEAD_PAIR_SOKRS.Contains(item))
                return RuleAction.Join;

            if (Sokr.PAIR_SOKRS.Contains(item))
            {
                if (IsSokr(right))
                    return RuleAction.Join;
                else
                    return RuleAction.None;
            }
        }

        var left = split.LeftToken!.ToLower();
        if (Sokr.HEAD_SOKRS.Contains(left))
            return RuleAction.Join;

        if (Sokr.SOKRS.Contains(left) && IsSokr(right))
            return RuleAction.Join;

        return RuleAction.None;
    }

    /// <summary>
    /// Проверить, что токен является сокращением
    /// </summary>
    /// <param name="token"> Токен </param>
    /// <returns></returns>
    private bool IsSokr(string token)
    {
        if (token.All(char.IsDigit))
            return true;

        if (!token.All(char.IsLetter))
            return true;

        return token.All(char.IsLower);
    }
}

/// <summary>
/// Правило "парное сокращение":
/// </summary>
/// <remarks>
/// МН. Ч.
/// </remarks>
public class InsidePairSokrRule : Rule<SentSplit>
{
    /// <inheritdoc/>
    public override RuleAction Check(SentSplit split)
    {
        if (split.Delimiter != ".")
            return RuleAction.None;

        var left = split.LeftToken!.ToLower();
        var right = split.RightToken!.ToLower();

        if (Sokr.PAIR_SOKRS.Contains((left, right)))
            return RuleAction.Join;

        return RuleAction.None;
    }
}

/// <summary>
/// Правило "инициалы слева":
/// </summary>
/// <remarks>
/// Ф. Э. Дзержинский
/// </remarks>
public class InitialsLeftRule : Rule<SentSplit>
{
    /// <inheritdoc/>
    public override RuleAction Check(SentSplit split)
    {
        if (split.Delimiter != ".")
            return RuleAction.None;

        var left = split.LeftToken!;
        if (left.Length == 1 && char.IsUpper(left[0]))
            return RuleAction.Join;

        if (Sokr.INITIALS.Contains(left.ToLower()))
            return RuleAction.Join;

        return RuleAction.None;
    }
}

/// <summary>
/// Правило "элемент списка":
/// </summary>
/// <remarks>
/// 1. нумерованный список
/// б. маркеры - буквы
/// III) маркеры - римские числа
/// </remarks>
public partial class ListItemRule : Rule<SentSplit>
{
    private const string BULLET_BOUNDS = ".)";
    private const int BULLET_SIZE = 20;
    private readonly HashSet<char> BULLET_CHARS = [.. "§абвгдеabcdef"];
    private static readonly Regex ROMAN = RomanRegex();

    /// <inheritdoc/>
    public override RuleAction Check(SentSplit split)
    {
        if (!BULLET_BOUNDS.Contains(split.Delimiter!))
            return RuleAction.None;

        if (split?.Buffer?.Length > BULLET_SIZE)
            return RuleAction.None;

        if (split!.BufferTokens.All(IsBullet))
            return RuleAction.Join;

        return RuleAction.None;
    }

    /// <summary>
    /// Проверить, что токен являтся возможным маркером списка
    /// </summary>
    /// <param name="token"> Токен </param>
    /// <returns></returns>
    private bool IsBullet(string token)
    {
        if (token.All(char.IsDigit)) return true;
        if (BULLET_BOUNDS.Contains(token)) return true;
        if (BULLET_CHARS.Contains(char.ToLower(token[0]))) return true;
        if (ROMAN.IsMatch(token)) return true;

        return false;
    }

    [GeneratedRegex(@"^[IVXML]+$", RegexOptions.CultureInvariant)]
    private static partial Regex RomanRegex();
}

/// <summary>
/// Правило "тире справа":
/// </summary>
/// <remarks>
/// "Россия должна прирастать Сибирью" - Ваши слова, господин Президент.
/// </remarks>
public class DashRightRule : Rule<SentSplit>
{
    /// <inheritdoc/>
    public override RuleAction Check(SentSplit split)
    {
        if (!Punct.DASHES.Contains(split.RightToken!))
            return RuleAction.None;

        var right = split.RightWord;
        if (!string.IsNullOrEmpty(right) && IsLowerWord(right))
            return RuleAction.Join;

        return RuleAction.None;
    }

    /// <summary>
    /// Проверить, что токен является словом в нижнем регистре
    /// </summary>
    /// <param name="token"> Токен </param>
    /// <returns></returns>
    private bool IsLowerWord(string? token)
    {
        if (string.IsNullOrEmpty(token)) return false;
        return token.All(e => char.IsLetter(e) && char.IsLower(e));
    }
}

/// <summary>
/// Абстрактное правило для окончания внутри границы
/// </summary>
public abstract class CloseBoundRule : Rule<SentSplit>
{
    /// <summary>
    /// Проверить, что окончание предложения находится слева
    /// </summary>
    /// <param name="split"> Разделение </param>
    /// <returns></returns>
    protected RuleAction CloseBound(SentSplit split) =>
        Punct.ENDINGS.Contains(split.LeftToken!) ? RuleAction.None : RuleAction.Join;
}

/// <summary>
/// Правило "окончание внутри кавычек":
/// </summary>
/// <remarks>
/// В рамках проекта «Больше, чем любовь»|+|(среди ...
/// </remarks>
public class CloseQuoteRule : CloseBoundRule
{
    /// <inheritdoc/>
    public override RuleAction Check(SentSplit split)
    {
        var delimiter = split.Delimiter!;

        if (!Punct.QUOTES.Contains(delimiter))
            return RuleAction.None;

        if (Punct.CLOSE_QUOTES.Contains(delimiter))
            return CloseBound(split);

        if (Punct.GENERIC_QUOTES.Contains(delimiter))
        {
            if (!split.LeftSpaceSuffix)
                return CloseBound(split);
            else
                return RuleAction.Join;
        }

        return RuleAction.None;
    }
}

/// <summary>
/// Правило "окончание внутри скобок":
/// </summary>
/// <remarks>
/// Примера не нашел
/// <remarks>
public class CloseBracketRule : CloseBoundRule
{
    /// <inheritdoc/>
    public override RuleAction Check(SentSplit split)
    {
        if (Punct.CLOSE_BRACKETS.Contains(split.Delimiter!))
            return CloseBound(split);

        return RuleAction.None;
    }
}
