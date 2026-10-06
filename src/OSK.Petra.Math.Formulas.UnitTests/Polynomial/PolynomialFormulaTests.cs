using OSK.Petra.Math.Formulas.Polynomial;

namespace OSK.Petra.Math.Formulas.UnitTests.Polynomial;

public class PolynomialFormulaTests
{
    #region Calculate

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Calculate_NullOrEmptyTerms_ReturnsBaseValue(bool useNull)
    {
        // Arrange
        var calculator = new PolynomialFormula(useNull ? null! : []);

        // Act/Assert
        for (var i = 0; i < 10; i++)
        {
            var result = calculator.Calculate(i);
            Assert.Equal(i, result);
        }
    }

    [Fact]
    public void Calculate_Terms_SubstitueCountWithCoeffecients()
    {
        // Arrange
        PolynomialTerm[] terms = [new PolynomialTerm(2, 0), new PolynomialTerm(4, 2)];
        var calculator = new PolynomialFormula(terms);

        // Act/Assert
        for (var i = 0; i < 10; i++)
        {
            var result = calculator.Calculate(i);

            var expected = terms.Sum(term => term.Coefficient * System.Math.Pow(i, term.Power));
            Assert.Equal(expected, result);
        }
    }

    [Theory]
    [InlineData(2, 0, 0)]
    [InlineData(2, 1, 2)]
    [InlineData(2, 2, 4)]
    [InlineData(2, 3, 6)]
    [InlineData(4, 2, 8)]
    public void Calculate_Linear_ReturnsExpectedOutput(double coefficient, int value, double expectedValue)
    {
        // Arrange
        var calculator = PolynomialFormula.Linear(coefficient);

        // Act
        var result = calculator.Calculate(value);

        // Assert
        Assert.Equal(expectedValue, result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public void Calculate_Constant_ReturnsValue(int value)
    {
        // Arrange
        var calculator = PolynomialFormula.Constant(value);

        // Act
        var result = calculator.Calculate(value);

        // Assert
        Assert.Equal(value, result);
    }

    #endregion
}
