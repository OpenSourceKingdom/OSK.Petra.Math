namespace OSK.Petra.Math.Formulas.Stepwise;

/// <summary>
/// Represents a step tier in a stepwise function
/// </summary>
/// <param name="Value">The value for the step</param>
/// <param name="Output">The step's output</param>
public readonly record struct Step(double Value, double Output);
