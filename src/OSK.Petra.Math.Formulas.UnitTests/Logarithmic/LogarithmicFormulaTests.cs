using OSK.Petra.Math.Formulas.Logarithmic;

namespace OSK.Petra.Math.Formulas.UnitTests.Logarithmic;

public class LogarithmicFormulaTests
{
    #region Constructor Tests

    [Theory]
    [InlineData(0.0)]
    [InlineData(1.0)]
    [InlineData(-5.0)]
    public void Constructor_InvalidLogBase_ThrowsInvalidOperationException(double invalidBase)
    {
        Assert.Throws<InvalidOperationException>(() => new LogarithmicFormula(2.0, invalidBase));
    }

    #endregion

    #region Calculate

    [Theory]
    [InlineData(2.0, 10.0, 0.0)]
    [InlineData(2.0, 10.0, 0.5)]
    [InlineData(2.0, 10.0, 1.0)]
    [InlineData(2.0, 10.0, 10.0)]
    [InlineData(3.0, 10.0, 100)]
    public void Calculate_ReturnsExpectedResult(double coefficient, double logBase, double value)
    {
        // Arrange
        var formula = new LogarithmicFormula(coefficient, logBase);

        // Act
        double result = formula.Calculate(value);

        // Assert
        Assert.Equal(coefficient * System.Math.Log(value, logBase), result, precision: 4);
    }

    #endregion
}
