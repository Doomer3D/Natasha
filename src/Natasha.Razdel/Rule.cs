// source: https://github.com/natasha/razdel/blob/master/razdel/rule.py

namespace Natasha.Razdel;

/// <summary>
/// Правило
/// </summary>
/// <typeparam name="T"> Тип разделения </typeparam>
public abstract class Rule<T>
    where T : Split
{
    /// <summary>
    /// Наименование правила
    /// </summary>
    public virtual string Name => GetType().Name;

    /// <summary>
    /// Проверить правило
    /// </summary>
    /// <param name="split"> Разделение </param>
    /// <returns></returns>
    public abstract RuleAction Check(T split);
}
