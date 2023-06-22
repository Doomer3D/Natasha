// source: https://github.com/natasha/razdel/blob/master/razdel/segmenters/tokenize.py

namespace Natasha.Razdel.Tokenizer;

/// <summary>
/// разделение токенов
/// </summary>
public class TokenSplit : Split
{
    /// <summary>
    /// атомы слева
    /// </summary>
    public List<Atom> LeftAtoms { get; init; }

    /// <summary>
    /// атомы справа
    /// </summary>
    public List<Atom> RightAtoms { get; init; }

    /// <summary>
    /// первый атом слева
    /// </summary>
    public Atom Left1 => _left1.Value;

    /// <summary>
    /// второй атом слева
    /// </summary>
    public Atom? Left2 => _left2.Value;

    /// <summary>
    /// третий атом слева
    /// </summary>
    public Atom? Left3 => _left3.Value;

    /// <summary>
    /// первый атом справа
    /// </summary>
    public Atom Right1 => _right1.Value;

    /// <summary>
    /// второй атом справа
    /// </summary>
    public Atom? Right2 => _right2.Value;

    /// <summary>
    /// третий атом справа
    /// </summary>
    public Atom? Right3 => _right3.Value;

    private readonly Lazy<Atom> _left1;
    private readonly Lazy<Atom?> _left2;
    private readonly Lazy<Atom?> _left3;

    private readonly Lazy<Atom> _right1;
    private readonly Lazy<Atom?> _right2;
    private readonly Lazy<Atom?> _right3;

    /// <summary>
    /// конструктор
    /// </summary>
    /// <param name="left"> атомы слева </param>
    /// <param name="delimiter"> разделитель </param>
    /// <param name="right"> атомы справа </param>
    public TokenSplit(List<Atom> left, string delimiter, List<Atom> right)
    {
        LeftAtoms = left;
        RightAtoms = right;

        _left1 = new(() =>
        {
            return LeftAtoms[^1];
        });
        _left2 = new(() =>
        {
            return LeftAtoms.Count > 1 ? LeftAtoms[^2] : default;
        });
        _left3 = new(() =>
        {
            return LeftAtoms.Count > 2 ? LeftAtoms[^3] : default;
        });

        _right1 = new(() =>
        {
            return RightAtoms[0];
        });
        _right2 = new(() =>
        {
            return RightAtoms.Count > 1 ? RightAtoms[1] : default;
        });
        _right3 = new(() =>
        {
            return RightAtoms.Count > 2 ? RightAtoms[2] : default;
        });

        Left = _left1.Value.Text;
        Delimiter = delimiter;
        Right = _right1.Value.Text;
    }
}
