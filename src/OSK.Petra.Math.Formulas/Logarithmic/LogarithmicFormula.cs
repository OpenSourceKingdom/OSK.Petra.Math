using System;

namespace OSK.Petra.Math.Formulas.Logarithmic;

/// <summary>
/// Provide a logarithmec formula
/// </summary>
/// <param name="coefficient">The coefficient</param>
/// <param name="logBase">The base log</param>
public class LogarithmicFormula(double coefficient, double logBase = 10) : IFormula
{
    #region Variables

    private readonly double _base = logBase <= 1 ? throw new InvalidOperationException("Log base must be positive and greater than 1.") : logBase;

    #endregion

    #region IFormula Overrides

    /// <inheritdoc/>
    public double Calculate(double value)
        => coefficient * System.Math.Log(value, _base);

    #endregion
}
