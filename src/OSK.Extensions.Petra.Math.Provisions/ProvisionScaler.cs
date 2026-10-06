using OSK.Extensions.Petra.Math.Provisions.Models;
using OSK.Petra.Math.Formulas;
using OSK.Petra.Provisions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using OSK.Extensions.Petra.Math.Provisions;

namespace OSK.Extensions.Petra.Math.Provisions;

/// <summary>
/// A special scaler for collections of provisions
/// </summary>
public class ProvisionScaler: IProvisionScaler
{
    #region Variables

    private readonly ProvisionScalingStrategy? _defaultStrategy;

    private readonly Dictionary<Guid, ProvisionScalingStrategy> _provisionStrategies = [];

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a provision scaler that uses a standard formula for all provisions
    /// </summary>
    /// <param name="formula">The standard formula to use</param>
    /// <param name="scalingMode">Determines how the formula's scale factor is applied to a provision</param>
    /// <exception cref="ArgumentNullException">If the formula is null</exception>
    public ProvisionScaler(IFormula formula, ScalingMode scalingMode = ScalingMode.Replace)
    {
        if (formula is null)
        {
            throw new ArgumentNullException(nameof(formula));
        }

        _defaultStrategy = new ProvisionScalingStrategy(formula, scalingMode);
    }

    /// <summary>
    /// Creates a provision scaler that uses a collection of scaling strategies
    /// </summary>
    /// <param name="scalingStrategies">The scaling strategies to use</param>
    /// <exception cref="ArgumentNullException">If strategies is null</exception>
    public ProvisionScaler(IEnumerable<ProvisionScalingStrategy> scalingStrategies)
    {
        if (scalingStrategies is null)
        {
            throw new ArgumentNullException(nameof(scalingStrategies));
        }

        foreach (var strategy in scalingStrategies)
        {
            if (strategy.ProvisionDefinitionId is null)
            {
                _defaultStrategy = strategy;
            }
            else
            {
                _provisionStrategies[strategy.ProvisionDefinitionId.Value] = strategy;
            }
        }
    }

    #endregion

    #region IProvisionScaler

    /// <inheritdoc/>
    public IEnumerable<Provision> Scale(IEnumerable<Provision> baseProvisions, int count)
    {
        if (baseProvisions is null || !baseProvisions.Any())
        {
            yield break;
        }

        foreach (var baseProvision in baseProvisions)
        {
            var scalingStrategy = _provisionStrategies.TryGetValue(baseProvision.Id, out var calculator)
                ? calculator
                : _defaultStrategy;

            var calculatedValue = (float)(scalingStrategy?.Formula.Calculate(count) ?? baseProvision.Amount);

            yield return scalingStrategy?.ScalingMode switch
            {
                ScalingMode.Add => baseProvision.WithAmount(baseProvision.Amount + calculatedValue),
                ScalingMode.Subtract => baseProvision.WithAmount(baseProvision.Amount - calculatedValue),
                ScalingMode.Multiply => baseProvision.WithAmount(baseProvision.Amount * calculatedValue),
                ScalingMode.Divide => baseProvision.WithAmount(calculatedValue != 0 ? baseProvision.Amount / calculatedValue : baseProvision.Amount),
                _ => baseProvision.WithAmount(calculatedValue)
            };
        }
    }

    #endregion
}
