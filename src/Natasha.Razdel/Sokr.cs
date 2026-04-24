// source: https://github.com/natasha/razdel/blob/master/razdel/segmenters/sokr.py

namespace Natasha.Razdel;

/// <summary>
/// Сокращения
/// </summary>
public static partial class Sokr
{
    /// <summary>
    /// Завершающие сокращения
    /// </summary>
    public static readonly HashSet<string> TAIL_SOKRS = [.. ParseSokrs([
        "дес тыс млн млрд",
        "дол долл",
        "коп руб р",
        "проц",
        "га",
        "барр",
        "куб",
        "кв км",
        "см",
        "час мин сек",
        "в вв",
        "г гг",
        "с стр",
        "co corp inc",
        "изд ed",
        "др",
        "al"
    ])];

    /// <summary>
    /// Начальные сокращения
    /// </summary>
    public static readonly HashSet<string> HEAD_SOKRS = [.. ParseSokrs([
        "букв",
        "ст",
        "трад",
        "лат венг исп кат укр нем англ фр итал греч",
        "евр араб яп слав кит рус русск латв",
        "словацк хорв",
        "mr mrs ms dr vs",
        "св",
        "арх зав зам проф акад",
        "кн",
        "корр",
        "ред",
        "гр",
        "ср",
        "чл корр",
        "им",
        "тов",
        "нач пол",
        "chap",
        "п пп ст ч чч гл стр абз пт",
        "no",
        "просп пр ул ш г гор д стр к корп пер корп обл эт пом ауд оф ком комн каб",
        "домовлад лит",
        "т",
        "рп пос с х",
        "пл",
        "bd",
        "о оз",
        "р",
        "а",
        "обр",
        "ум",
        "ок",
        "откр",
        "пс ps",
        "upd",
        "см",
        "напр",
        "доп",
        "юр физ",
        "тел",
        "сб",
        "внутр",
        "дифф",
        "гос",
        "отм"
    ])];

    /// <summary>
    /// Прочие сокращения
    /// </summary>
    public static readonly HashSet<string> OTHER_SOKRS = [.. ParseSokrs([
        "сокр рис искл прим",
        "яз",
        "устар",
        "шутл"
    ])];

    /// <summary>
    /// Все сокращения
    /// </summary>
    public static readonly HashSet<string> SOKRS = Combine(TAIL_SOKRS, HEAD_SOKRS, OTHER_SOKRS);

    /// <summary>
    /// Завершающие парные сокращения
    /// </summary>
    public static readonly HashSet<(string, string)> TAIL_PAIR_SOKRS = [.. ParsePairSokrs([
        "т п",
        "т д",
        "у е",
        "н э",
        "p m",
        "a m",
        "с г",
        "р х",
        "с г",
        "с ш",
        "з д",
        "л с",
        "ч т",
        "т д"
    ])];

    /// <summary>
    /// Начальные парные сокращения
    /// </summary>
    public static readonly HashSet<(string, string)> HEAD_PAIR_SOKRS = [.. ParsePairSokrs([
        "т е",
        "т к",
        "т н",
        "и о",
        "к н",
        "к п",
        "п н",
        "к т",
        "т н",
        "л д"
    ])];

    /// <summary>
    /// Прочие парные сокращения
    /// </summary>
    public static readonly HashSet<(string, string)> OTHER_PAIR_SOKRS = [.. ParsePairSokrs([
        "ед ч",
        "мн ч",
        "повел накл",
        "жен р",
        "муж р"
    ])];

    /// <summary>
    /// Все парные сокращения
    /// </summary>
    public static readonly HashSet<(string, string)> PAIR_SOKRS = Combine(TAIL_PAIR_SOKRS, HEAD_PAIR_SOKRS, OTHER_PAIR_SOKRS);

    /// <summary>
    /// Инициалы
    /// </summary>
    public static readonly HashSet<string> INITIALS =
    [
        "дж",
        "ed",
        "вс"
    ];

    /// <summary>
    /// Распарсить сокращения
    /// </summary>
    /// <param name="lines"> Строки </param>
    /// <returns></returns>
    private static IEnumerable<string> ParseSokrs(IEnumerable<string> lines)
    {
        foreach (var line in lines)
        {
            foreach (var word in line.Split())
            {
                yield return word;
            }
        }
    }

    /// <summary>
    /// Распарсить парные сокращения
    /// </summary>
    /// <param name="lines"> Строки </param>
    /// <returns></returns>
    private static IEnumerable<(string, string)> ParsePairSokrs(IEnumerable<string> lines)
    {
        foreach (var line in lines)
        {
            var split = line.Split();
            yield return (split[0], split[1]);
        }
    }

    /// <summary>
    /// Комбинировать хешсеты
    /// </summary>
    /// <typeparam name="T"> Тип элемента </typeparam>
    /// <param name="sets"> Список хешсетов </param>
    /// <returns></returns>
    private static HashSet<T> Combine<T>(params HashSet<T>[] sets)
    {
        var result = new HashSet<T>();
        if (sets != null && sets.Length > 0)
        {
            foreach (var set in sets)
            {
                foreach (var item in set)
                {
                    result.Add(item);
                }
            }
        }
        return result;
    }
}
