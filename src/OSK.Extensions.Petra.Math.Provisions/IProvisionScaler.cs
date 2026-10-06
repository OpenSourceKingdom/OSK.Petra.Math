using OSK.Petra.Provisions.Models;
using System.Collections.Generic;

namespace OSK.Extensions.Petra.Calculators.Provisions;

/// <summary>
/// A scaler that is able to perform scaling for a variety of provisions
/// </summary>
public interface IProvisionScaler
{
    /// <summary>
    /// Scales the provided provisions based on the configuration
    /// </summary>
    /// <param name="baseProvisions">The initial base provisions to be scaled</param>
    /// <param name="count">The count/iteration for the scaling</param>
    /// <returns>The scaled provisions</returns>
    IEnumerable<Provision> Scale(IEnumerable<Provision> baseProvisions, int count);
}
