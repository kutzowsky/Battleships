using Battleships.Core;
using FluentAssertions;
using Xunit;

namespace BattleShips.Core.Tests
{
    public class FieldTests
    {
        Field _field;

        public FieldTests()
        {
            _field = new Field();
        }

        [Fact]
        public void Field_WhenInitialized_SholdHaveEmptyState()
        {
            _field.State.Should().Be(FieldState.EMPTY);
        }

        [Fact]
        public void Field_WhenInitialized_SholdHaveEmptyShipId()
        {
            _field.ShipId.Should().BeNull();
        }

        [Fact]
        public void PlaceShip_WhenCalled_ShouldChangeFieldStateToShip()
        {
            _field.PlaceShip(1);

            _field.State.Should().Be(FieldState.SHIP);
        }

        [Fact]
        public void PlaceShip_WhenCalled_ShouldSetProperShipId()
        {
            var shipId = 1;

            _field.PlaceShip(shipId);

            _field.ShipId.Should().Be(shipId);
        }

    }
}
