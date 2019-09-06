using Battleships.Core;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BattleShips.Core.Tests
{
    public class ShipTests
    {
        [Fact]
        public void Ship_WhenInitialized_ShouldHaveUniqueId()
        {
            var firstShip = new Ship();
            var secondShip = new Ship();
            var thirdShip = new Ship();

            var shipList = new List<Ship> { firstShip, secondShip, thirdShip };

            shipList.Select(ship => ship.Id).Should().OnlyHaveUniqueItems();
        }

        [Fact]
        public void Hit_WhenCalled_ShouldReduceShipLength()
        {
            var ship = new Ship
            {
                Length = 4
            };

            ship.Hit();

            ship.Length.Should().Be(3);
        }

        [Fact]
        public void Hit_WhenCalledAndShipLengthIsZero_ShouldLeaveItAsZero()
        {
            var ship = new Ship
            {
                Length = 0
            };

            ship.Hit();

            ship.Length.Should().Be(0);
        }

        [Fact]
        public void Destroyed_WhenShipLengthIsZero_ShouldReturnTrue()
        {
            var ship = new Ship
            {
                Length = 0
            };

            ship.Destroyed.Should().BeTrue();
        }

        [Fact]
        public void Destroyed_WhenShipLengthGreaterThanZero_ShouldReturnFalse()
        {
            var ship = new Ship
            {
                Length = 4
            };

            ship.Destroyed.Should().BeFalse();
        }
    }
}
