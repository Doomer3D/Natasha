namespace Natasha.Razdel;

/// <summary>
/// результат действия правила
/// </summary>
public enum RuleAction
{
    /// <summary>
    /// действие не определено
    /// </summary>
    None = 0,

    /// <summary>
    /// объединение
    /// </summary>
    Join = 1,

    /// <summary>
    /// разделение
    /// </summary>
    Split = 2,
}
