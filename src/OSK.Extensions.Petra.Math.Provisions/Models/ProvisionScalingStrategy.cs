using OSK.Petra.Math.Formulas;
using System;

namespace OSK.Extensions.Petra.Math.Provisions.Models;

/// <summary>
/// Provides information to a scaling strategy that will be used for a particular provision
/// </summary>
public readonly struct ProvisionScalingStrategy
{
    #region Variables

    /// <summary>
    /// The provision definition the scaling is associated with
    /// </summary>
    /// <remarks>
    /// 💡Notes:
    /// <list type="bullet">
    /// <item>A null definition id indicates that the strategy is a default/fallback strategy</item>
    /// </list>
    /// </remarks>
    public Guid? ProvisionDefinitionId { get; }

    /// <summary>
    /// The formula to utilize with the provision
    /// </summary>
    public IFormula Formula { get; }

    /// <summary>
    /// Determines how the formula's scale factor is applied to a provisio
    /// </summary>
    public ScalingMode ScalingMode { get; }

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a provision strategy that is a fallback/default strategy and can be applied to any provision
    /// </summary>
    /// <param name="formula">The formula to use</param>
    /// <param name="scalingMode">Determines how the formula's scale factor is applied to a provisio</param>
    /// <exception cref="ArgumentNullException">If the formula is null</exception>
    public ProvisionScalingStrategy(IFormula formula, ScalingMode scalingMode = ScalingMode.Replace)
    {
        Formula = formula ?? throw new ArgumentNullException(nameof(formula));
        ScalingMode = scalingMode;
    }

    /// <summary>
    /// Creates a provision strategy that is targeted to the specific provision with the provided formula
    /// </summary>
    /// <param name="provisionDefinitionId">The target provisiont to use this strategy for</param>
    /// <param name="formula">The formula to use</param>
    /// <param name="scalingMode">Determines how the formula's scale factor is applied to a provisio</param>
    /// <exception cref="ArgumentNullException">IF the formula is null</exception>
    public ProvisionScalingStrategy(Guid provisionDefinitionId, IFormula formula, ScalingMode scalingMode = ScalingMode.Replace)
    {
        ProvisionDefinitionId = provisionDefinitionId;
        Formula = formula ?? throw new ArgumentNullException(nameof(formula));
        ScalingMode = scalingMode;
    }

    #endregion
}
