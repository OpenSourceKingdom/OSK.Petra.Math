using System.Collections.Generic;
using System.Linq;

namespace OSK.Petra.Math.Formulas.Stepwise;

/// <summary>
/// Provides a step wise formula
/// </summary>
public class StepwiseFormula : IFormula
{
    #region Variables

    private readonly Step[] _steps;

    private int _stepIndex = 0;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a stepwise formula that uses a collection of <see cref="Step"/>
    /// </summary>
    /// <param name="steps">The collection of steps</param>
    public StepwiseFormula(IEnumerable<Step> steps)
    {
        _steps = steps?.OrderBy(step => step.Value).ToArray() ?? [];
    }

    /// <summary>
    /// Creates a stepwise formula that uses a parameter list of <see cref="Step"/>
    /// </summary>
    /// <param name="steps">The collection of steps</param>
    public StepwiseFormula(params Step[] steps)
    {
        _steps = steps?.OrderBy(step => step.Value).ToArray() ?? [];
    }

    #endregion

    #region IFormula

    /// <inheritdoc/>
    public double Calculate(double value)
    {
        if (_steps is null || _steps.Length is 0)
        {
            return value;
        }

        var step = _steps[_stepIndex];
        if (step.Value > value)
        {
            while (_stepIndex > 0 && _steps[_stepIndex - 1].Value >= value)
            {
                _stepIndex--;
                step = _steps[_stepIndex];
            }
        }
        else if (step.Value < value)
        {
            while (_stepIndex < _steps.Length - 1 && _steps[_stepIndex + 1].Value <= value)
            {
                _stepIndex++;
                step = _steps[_stepIndex];
            }
        }

        return step.Value > value
            ? value
            : step.Output;
    }

    #endregion
}
