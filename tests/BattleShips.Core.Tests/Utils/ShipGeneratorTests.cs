using Battleships.Core.Enums;
using Battleships.Core.Utils.Interfaces;
using FluentAssertions;
using Xunit;

namespace Battleships.Core.Utils
{
    public class ShipGeneratorTests
    {
        IShipGenerator _shipGenerator;

        public ShipGeneratorTests()
        {
            _shipGenerator = new ShipGenerator();
        }

        [Fact]
        public void CreateDestroyer_WhenCalled_ShoulCreateShipWithProperNameAndLength()
        {
            var destroyer = _shipGenerator.CreateDestroyer();

            destroyer.Type.Should().Be(ShipType.DESTROYER);
            destroyer.Length.Should().Be(4);
        }

        [Fact]
        public void CreateBattleship_WhenCalled_ShoulCreateShipWithProperNameAndLength()
        {
            var battleship = _shipGenerator.CreateBattleship();

            battleship.Type.Should().Be(ShipType.BATTLESHIP);
            battleship.Length.Should().Be(5);
        }
    }
}
