namespace OSK.Petra.Math.Formulas.Exponential;

/// <summary>
/// Provides an exponential formula
/// </summary>
/// <param name="constant">The constant numeric value to apply to the exponential equation</param>
/// <param name="mode">The calculation mode for the constant</param>
public class ExponentialFormula(double constant) : IFormula
{
    #region IFormula Overrides

    /// <inheritdoc/>
    public double Calculate(double value)
        => System.Math.Pow(constant, value);

    #endregion
}
