using System.Drawing;
using Battleships.Core;
using FluentAssertions;
using Xunit;

namespace BattleShips.Core.Tests
{
    public class BoardTests
    {
        readonly Board _board;

        public BoardTests()
        {
            _board = new Board();
        }

        [Fact]
        public void BoardClass_ShouldExist()
        {
            _board.Should().NotBeNull();
        }

        [Fact]
        public void Board_WhenInitialized_ShouldHaveEmptyFields()
        {
            _board.Fields[0, 0].Should().Be(Field.EMPTY);
            _board.Fields[2, 7].Should().Be(Field.EMPTY);
            _board.Fields[5, 5].Should().Be(Field.EMPTY);
            _board.Fields[9, 9].Should().Be(Field.EMPTY);
        }

        [Fact]
        public void Place_WhenCalledWithHorizontalShip_ShouldMarkPinsCorrectly()
        {
            var ship = new Ship
            {
                Length = 4,
                StartingPoint = new Point(0, 0),
                Orientation = ShipOrientation.HORIZONTAL
            };

            _board.Place(ship);

            _board.Fields[0, 0].Should().Be(Field.SHIP);
            _board.Fields[1, 0].Should().Be(Field.SHIP);
            _board.Fields[2, 0].Should().Be(Field.SHIP);
            _board.Fields[3, 0].Should().Be(Field.SHIP);

            _board.Fields[4, 0].Should().Be(Field.EMPTY);
            _board.Fields[0, 1].Should().Be(Field.EMPTY);
        }

        [Fact]
        public void Place_WhenCalledWithVerticalShip_ShouldMarkPinsCorrectly()
        {
            var ship = new Ship
            {
                Length = 5,
                StartingPoint = new Point(5, 1),
                Orientation = ShipOrientation.VERTICAL
            };

            _board.Place(ship);

            _board.Fields[5, 1].Should().Be(Field.SHIP);
            _board.Fields[5, 2].Should().Be(Field.SHIP);
            _board.Fields[5, 3].Should().Be(Field.SHIP);
            _board.Fields[5, 4].Should().Be(Field.SHIP);
            _board.Fields[5, 5].Should().Be(Field.SHIP);

            _board.Fields[5, 0].Should().Be(Field.EMPTY);
            _board.Fields[5, 6].Should().Be(Field.EMPTY);
        }

        [Fact]
        public void Shoot_OnHit_ShouldMarkPinsCorrectly()
        {
            var ship = new Ship
            {
                Length = 4,
                StartingPoint = new Point(0, 0),
                Orientation = ShipOrientation.HORIZONTAL
            };

            var shotX = 2;
            var shotY = 0;
            var shot = new Point(shotX, shotY);

            _board.Place(ship);

            _board.Shoot(shot);

            _board.Fields[shotX, shotY].Should().Be(Field.HIT);
        }

        [Fact]
        public void Shoot_OnMiss_ShouldMarkPinsCorrectly()
        {
            var ship = new Ship
            {
                Length = 4,
                StartingPoint = new Point(0, 0),
                Orientation = ShipOrientation.HORIZONTAL
            };

            var shotX = 1;
            var shotY = 2;
            var shot = new Point(shotX, shotY);

            _board.Place(ship);

            _board.Shoot(shot);

            _board.Fields[shotX, shotY].Should().Be(Field.MISS);
        }
    }
}
