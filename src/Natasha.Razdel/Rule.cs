// source: https://github.com/natasha/razdel/blob/master/razdel/rule.py

namespace Natasha.Razdel;

/// <summary>
/// правило
/// </summary>
/// <typeparam name="T"> тип разделения </typeparam>
public abstract class Rule<T>
    where T : Split
{
    /// <summary>
    /// наименование правила
    /// </summary>
    public virtual string Name => GetType().Name;

    /// <summary>
    /// проверить правило
    /// </summary>
    /// <param name="split"> разделение </param>
    /// <returns></returns>
    public abstract RuleAction Check(T split);
}
