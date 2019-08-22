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
            _board.Fields[0, 0].State.Should().Be(FieldState.EMPTY);
            _board.Fields[2, 7].State.Should().Be(FieldState.EMPTY);
            _board.Fields[5, 5].State.Should().Be(FieldState.EMPTY);
            _board.Fields[9, 9].State.Should().Be(FieldState.EMPTY);
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

            _board.Fields[0, 0].State.Should().Be(FieldState.SHIP);
            _board.Fields[1, 0].State.Should().Be(FieldState.SHIP);
            _board.Fields[2, 0].State.Should().Be(FieldState.SHIP);
            _board.Fields[3, 0].State.Should().Be(FieldState.SHIP);
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

            _board.Fields[5, 1].State.Should().Be(FieldState.SHIP);
            _board.Fields[5, 2].State.Should().Be(FieldState.SHIP);
            _board.Fields[5, 3].State.Should().Be(FieldState.SHIP);
            _board.Fields[5, 4].State.Should().Be(FieldState.SHIP);
            _board.Fields[5, 5].State.Should().Be(FieldState.SHIP);
        }

        [Fact]
        public void Place_WhenCalled_ShouldSetShipIdOnAllShipFields()
        {
            var ship = new Ship
            {
                Length = 4,
                StartingPoint = new Point(0, 0),
                Orientation = ShipOrientation.HORIZONTAL
            };

            _board.Place(ship);

            _board.Fields[0, 0].ShipId.Should().NotBeNull();
            _board.Fields[1, 0].ShipId.Should().NotBeNull();
            _board.Fields[2, 0].ShipId.Should().NotBeNull();
            _board.Fields[3, 0].ShipId.Should().NotBeNull();
        }

        [Fact]
        public void Shoot_OnHit_ShouldMarkPinCorrectly()
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

            _board.Fields[shotX, shotY].State.Should().Be(FieldState.HIT);
        }

        [Fact]
        public void Shoot_OnMiss_ShouldMarkPinCorrectly()
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

            _board.Fields[shotX, shotY].State.Should().Be(FieldState.MISS);
        }
    }
}
