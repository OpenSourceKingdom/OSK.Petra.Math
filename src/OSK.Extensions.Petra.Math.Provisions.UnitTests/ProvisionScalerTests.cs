using Moq;
using OSK.Extensions.Petra.Math.Provisions.Models;
using OSK.Petra.Math.Formulas;
using OSK.Petra.Provisions.Models;
using OSK.Extensions.Petra.Math.Provisions;

namespace OSK.Extensions.Petra.Math.Provisions.UnitTests;

public class ProvisionScalerTests
{
    #region Variables

    private readonly Mock<IFormula> _mockFormula;

    private readonly ProvisionScaler _scaler;

    #endregion

    #region Constructors

    public ProvisionScalerTests()
    {
        _mockFormula = new();

        _scaler = new(_mockFormula.Object);
    }

    #endregion

    #region Constructors Tests

    [Fact]
    public void Constructor_WithNullDefaultFormula_ThrowsArgumentNullException()
    {
        // Arrange/Act/Assert
        Assert.Throws<ArgumentNullException>(() => new ProvisionScaler((IFormula)null!));
    }

    [Fact]
    public void Constructor_WithNullStrategiesCollection_ThrowsArgumentNullException()
    {
        // Arrange/Act/Assert
        Assert.Throws<ArgumentNullException>(() => new ProvisionScaler((IEnumerable<ProvisionScalingStrategy>)null!));
    }

    #endregion

    #region Scale

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Scale_WithNullOrEmptyBaseProvisions_ReturnsEmpty(bool useNull)
    {
        // Arrange/Act
        var result = _scaler.Scale(useNull ? null! : [], 1);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void Scale_UsingDefaultFormula_WhenNoSpecificStrategyExists_AppliesDefaultFormula()
    {
        // Arrange
        var provisionId = Guid.NewGuid();
        var baseProvisions = new[] { new Provision(provisionId, 50) };

        _mockFormula.Setup(c => c.Calculate(It.IsAny<double>())).Returns(75.5);

        // Act
        var result = _scaler.Scale(baseProvisions, 3).ToList();

        // Assert
        Assert.Single(result);

        Assert.Equal(provisionId, result[0].Id);
        Assert.Equal(75.5d, result[0].Amount);
    }

    [Theory]
    [InlineData(ScalingMode.Replace)]
    [InlineData(ScalingMode.Add)]
    [InlineData(ScalingMode.Subtract)]
    [InlineData(ScalingMode.Multiply)]
    [InlineData(ScalingMode.Divide)]
    public void Scale_UsingSpecificProvisionStrategy_DifferentScalingModes_WhenStrategyExists_AppliesSpecificFormula(ScalingMode scalingMode)
    {
        // Arrange
        var goldId = Guid.NewGuid();
        var lumberId = Guid.NewGuid();

        var mockGoldFormula = new Mock<IFormula>();
        mockGoldFormula.Setup(c => c.Calculate(It.IsAny<double>()))
            .Returns(120.0);

        var strategies = new List<ProvisionScalingStrategy>
        {
            new(goldId, mockGoldFormula.Object)
        };

        var baseProvisions = new[]
        {
            new Provision(goldId, 100),
            new Provision(lumberId, 50)
        };

        var Formula = new ProvisionScaler(strategies);

        // Act
        var result = Formula.Scale(baseProvisions, 2).ToList();

        // Assert
        Assert.Equal(2, result.Count);

        var baseAmount = baseProvisions[0].Amount;
        var expectedAmount = scalingMode switch
        {
            ScalingMode.Add => baseAmount + 120,
            ScalingMode.Subtract => baseAmount - 120,
            ScalingMode.Multiply => baseAmount * 120,
            ScalingMode.Divide => baseAmount / 120,
            _ => 120
        };

        // Gold used specific Formula
        Assert.Equal(goldId, result[0].Id);
        Assert.Equal(120, result[0].Amount);;

        // Lumber had no Formula (default is null), so amount remains unScaled
        Assert.Equal(lumberId, result[1].Id);
        Assert.Equal(50, result[1].Amount);

        mockGoldFormula.Verify(c => c.Calculate(2), Times.Once);
    }

    [Fact]
    public void Constructor_WithStrategiesContainingDefaultAndSpecific_ConfiguresCorrectly()
    {
        // Arrange
        var manaId = Guid.NewGuid();
        var mockDefault = new Mock<IFormula>();
        var mockManaFormula = new Mock<IFormula>();

        mockDefault.Setup(c => c.Calculate(It.IsAny<double>())).Returns(15.0);
        mockManaFormula.Setup(c => c.Calculate(It.IsAny<double>())).Returns(30.0);

        var strategies = new List<ProvisionScalingStrategy>
        {
            // Null ProvisionDefinitionId sets the default Formula in this constructor overload
            new(mockDefault.Object),
            new(manaId, mockManaFormula.Object)
        };

        var baseProvisions = new[]
        {
            new Provision(Guid.NewGuid(), 10),
            new Provision(manaId, 20)
        };

        var Formula = new ProvisionScaler(strategies);

        // Act
        var result = Formula.Scale(baseProvisions, 1).ToList();

        // Assert
        Assert.Equal(2, result.Count);

        Assert.Equal(15, result[0].Amount);
        Assert.Equal(30, result[1].Amount);
    }

    #endregion
}
