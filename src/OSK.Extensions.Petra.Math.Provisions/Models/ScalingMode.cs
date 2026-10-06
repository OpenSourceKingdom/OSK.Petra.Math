namespace OSK.Extensions.Petra.Math.Provisions.Models;

/// <summary>
/// Specifies different modes for handling a formula's scale factor
/// </summary>
public enum ScalingMode
{   
    /// <summary>
    /// The scale factor replaces the original value completely
    /// </summary>
    Replace,

    /// <summary>
    /// The scale factor should be added against the original value
    /// </summary>
    Add,

    /// <summary>
    /// The scale factor should be subtracted against the original value
    /// </summary>
    Subtract,

    /// <summary>
    /// The scale factor should be multiplied against the original value
    /// </summary>
    Multiply,

    /// <summary>
    /// The scale factor should be divided against the original value
    /// </summary>
    Divide
}
