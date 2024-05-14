using DistanceCalculator.Domain.Calculators;
using DistanceCalculator.Domain.Enums;
using DistanceCalculator.Domain.Models;

namespace DistanceCalculator.Domain.Tests.Calculators;

public class DistanceCalculatorTests
{
    [Fact]
    public void Calculate_HasStrategy_UsesGivenStrategy()
    {
        // Arrange
        const CalculationType calculationType = CalculationType.Haversine;
        var strategy = new Mock<IDistanceCalculationStrategy>();
        strategy.Setup(x => x.Type).Returns(calculationType);
        strategy.Setup(x => x.Calculate(It.IsAny<Coordinate>(), It.IsAny<Coordinate>())).Returns(10000);

        var strategies = new List<IDistanceCalculationStrategy> { strategy.Object };
        var calculator = new Domain.Calculators.DistanceCalculator(strategies);

        var coordinateA = new Coordinate(0, 0);
        var coordinateB = new Coordinate(0, 1);
        
        // Act
        var distance = calculator.Calculate(coordinateA, coordinateB, calculationType);

        // Assert
        distance.Should().NotBeNull();
        distance.Meters.Should().Be(10000);
        strategy.Verify(x => x.Calculate(coordinateA, coordinateB), Times.Once);
    }
    
    [Fact]
    public void Calculate_HasMultipleStrategiesDefinedForOneType_ThrowsException()
    {
        // Arrange
        const CalculationType calculationType = CalculationType.Haversine;
        var strategy = new Mock<IDistanceCalculationStrategy>();
        strategy.Setup(x => x.Type).Returns(calculationType);
        strategy.Setup(x => x.Calculate(It.IsAny<Coordinate>(), It.IsAny<Coordinate>())).Returns(10000);

        var strategies = new List<IDistanceCalculationStrategy> { strategy.Object, strategy.Object };
        var calculator = new Domain.Calculators.DistanceCalculator(strategies);

        var coordinateA = new Coordinate(0, 0);
        var coordinateB = new Coordinate(0, 1);

        // Act
        var func = () => calculator.Calculate(coordinateA, coordinateB, calculationType);

        // Assert
        func.Should()
            .ThrowExactly<InvalidOperationException>()
            .WithMessage("Sequence contains more than one matching element");
    }
}