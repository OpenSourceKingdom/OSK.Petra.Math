using OSK.Petra.Math.Formulas.Exponential;

namespace OSK.Petra.Math.Formulas.UnitTests.Exponential;

public class ExponentialFormulaTests
{
    #region Calculate

    [Theory]
    [InlineData(5, 4)]
    [InlineData(2, 6)]
    public void Calculate_Exponential_GrowsAsExpected(double constant, int value)
    {
        // Arrange
        var calculator = new ExponentialFormula(constant);

        // Act
        var result = calculator.Calculate(value);

        // Assert
        var result = System.Math.Pow(constant, value);

        Assert.Equal(result, result);
    }

    #endregion
}
