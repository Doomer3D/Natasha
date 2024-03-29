# Natasha

Порт библиотеки https://github.com/natasha на C#

Поддерживаемые библиотеки:

* <a href="https://github.com/natasha/razdel">natasha.razdel</a>

## Natasha.Razdel

[![stable](https://img.shields.io/nuget/v/Natasha.Razdel.svg?label=stable)](https://www.nuget.org/packages/Natasha.Razdel/)

### Sentenizer

Использование:

```cs
var text = """
- "Так в чем же дело?" - "Не ра-ду-ют".
И т. д. и т. п. В общем, вся газета
""";

var sentenizer = new TextSentenizer();
var data = sentenizer.Split(text).ToList();

// [0:22] - "Так в чем же дело?"
// [23:39] - "Не ра-ду-ют".
// [41:56] И т. д. и т. п.
// [57:76] В общем, вся газета
```

### Tokenizer

Использование:

```cs
var text = "Кружка-термос на 0.5л (50/64 см³, 516;...)";
var tokenizer = new TextTokenizer();
var data = tokenizer.Split(text).ToList();

// [0:13] Кружка-термос
// [14:16] на
// [17:20] 0.5
// [20:21] л
// [22:23] (
// [23:28] 50/64
// [29:32] см?
// [32:33] ,
// [34:37] 516
// [37:38] ;
// [38:41] ...
// [41:42] )
```

Отличия от оригинала:
1. В рамках импортозамещения удалено правило `yahoo!`
2. Добавлена возможность модифицировать список правил и создавать собственные

Пример реализации собственного правила:

```cs
/// <summary>
/// правило присоединения символа номера (№) к числу справа
/// </summary>
class NumberRule : Rule<TokenSplit>
{
    public override RuleAction Check(TokenSplit split)
    {
        var res = split.Left == "№" && split.Right1.Type == AtomType.Int;
        return res ? RuleAction.Join : RuleAction.None;
    }
}

...

tokenizer.Rules.Add(new NumberRule());
```
