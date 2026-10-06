namespace OSK.Petra.Math.Formulas.Polynomial;

/// <summary>
/// Represents a term of the polynomial equation
/// </summary>
/// <param name="Coefficient">The term coeffecient</param>
/// <param name="Power">The term power</param>
public readonly record struct PolynomialTerm(double Coefficient, int Power);
