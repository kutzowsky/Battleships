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
    }
}
