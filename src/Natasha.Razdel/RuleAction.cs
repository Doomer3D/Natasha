namespace Natasha.Razdel;

/// <summary>
/// Результат действия правила
/// </summary>
public enum RuleAction
{
    /// <summary>
    /// Действие не определено
    /// </summary>
    None = 0,

    /// <summary>
    /// Объединение
    /// </summary>
    Join = 1,

    /// <summary>
    /// Разделение
    /// </summary>
    Split = 2,
}
