using OSK.Petra.Math.Formulas.Stepwise;

namespace OSK.Petra.Math.Formulas.UnitTests.Stepwise;

public class StepwiseFormulaTests
{
    #region Calculate

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Calculate_NullOrEmptySteps_ReturnsStartingValue(bool useNull)
    {
        // Arrange
        var calculator = new StepwiseFormula(useNull ? null! : []);

        // Act/Assert
        for (var i = 0; i < 10; i++)
        {
            var result = calculator.Calculate(i);

            Assert.Equal(i, result);
        }
    }

    [Fact]
    public void Calculate_ValidSteps_ReturnsValueMeetingStepCriteria()
    {
        // Arrange
        var steps = new Step[]
        {
            new Step(2, 2),
            new Step(4, 5),
            new Step(8, 6),
            new Step(10, 8)
        };

        var calculator = new StepwiseFormula(steps);

        // Act/Assert
        for (var i = 0; i < 15; i++)
        {
            var result = calculator.Calculate(i);

            if (i < 2)
            {
                Assert.Equal(i, result);
            }
            else if (i < 4)
            {
                Assert.Equal(2, result);
            }
            else if (i < 8)
            {
                Assert.Equal(5, result);
            }
            else if (i < 10)
            {
                Assert.Equal(6, result);
            }
            else
            {
                Assert.Equal(8, result);
            }
        }
    }

    #endregion
}
