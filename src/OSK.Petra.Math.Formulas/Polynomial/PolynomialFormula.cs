using System.Collections.Generic;
using System.Linq;

namespace OSK.Petra.Math.Formulas.Polynomial;

/// <summary>
/// Provides a polynomial formula
/// </summary>
public class PolynomialFormula: IFormula
{
    #region Static

    /// <summary>
    /// Creates a polynomial that only uses a base value and constant
    /// </summary>
    /// <param name="constant">The constant to add</param>
    /// <returns>The calculator</returns>
    public static PolynomialFormula Constant(double constant = 0)
        => new([new PolynomialTerm(constant, 0)]);

    /// <summary>
    /// Creates a linear polynomial that only uses a single coeffecient and constant
    /// </summary>
    /// <param name="coeffecient">The coefficient for the linear term</param>
    /// <param name="constant">The constant to add</param>
    /// <returns>The calculator</returns>
    public static PolynomialFormula Linear(double coeffecient, double constant = 0)
        => new([new PolynomialTerm(constant, 0), new PolynomialTerm(coeffecient, 1)]);

    /// <summary>
    /// Creates a polynomial that uses the given coeffecients in the order they appear as index power terms with an additive constant
    /// </summary>
    /// <param name="coeffecients">The coefficients of the terms</param>
    /// <returns>The calculator</returns>
    public static PolynomialFormula Create(params double[] coeffecients)
        => new(coeffecients.Select((coeffecient, index) => new PolynomialTerm(coeffecient, index)));

    #endregion

    #region Variables

    private readonly ICollection<PolynomialTerm> _terms;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a polynomial polynomial using the specified terms and constant
    /// </summary>
    /// <param name="terms">The collection of polynomial terms</param>
    public PolynomialFormula(IEnumerable<PolynomialTerm> terms)
    {
        _terms = terms?.ToArray() ?? [];
    }

    #endregion

    #region IFormula

    /// <inheritdoc/>
    public double Calculate(double value)
        => _terms is { Count :> 0 } ? _terms.Sum(term => term.Coefficient * System.Math.Pow(value, term.Power)) : value;

    #endregion
}
