namespace OSK.Petra.Math.Formulas;

/// <summary>
/// A mathematical formula that provides a calculated result
/// </summary>
public interface IFormula
{
    /// <summary>
    /// Calculates an output provided a value
    /// </summary>
    /// <param name="value">The input value</param>
    /// <returns>An output value from the formula</returns>
    double Calculate(double value);
}
