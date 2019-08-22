using System;
using System.Drawing;
using Battleships.Core;
using Battleships.Core.Enums;
using Battleships.Core.Interfaces;
using FluentAssertions;
using Xunit;

namespace BattleShips.Core.Tests
{
    public class BoardTests
    {
        readonly IBoard _board;

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
            var ship = BuildSampleHorizontalShip();

            _board.Place(ship);

            _board.Fields[0, 0].State.Should().Be(FieldState.SHIP);
            _board.Fields[1, 0].State.Should().Be(FieldState.SHIP);
            _board.Fields[2, 0].State.Should().Be(FieldState.SHIP);
            _board.Fields[3, 0].State.Should().Be(FieldState.SHIP);
        }

        [Fact]
        public void Place_WhenCalledWithVerticalShip_ShouldMarkPinsCorrectly()
        {
            var ship = BuildSampleVerticalShip();

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
            var ship = BuildSampleHorizontalShip();

            _board.Place(ship);

            _board.Fields[0, 0].ShipId.Should().Be(ship.Id);
            _board.Fields[1, 0].ShipId.Should().Be(ship.Id);
            _board.Fields[2, 0].ShipId.Should().Be(ship.Id);
            _board.Fields[3, 0].ShipId.Should().Be(ship.Id);
        }

        [Fact]
        public void Place_WhenCalled_ShouldAddShipToTheShipList()
        {
            var ship = BuildSampleHorizontalShip();

            _board.Place(ship);

            _board.Ships.Should().HaveCount(1);
            _board.Ships.Should().Contain(ship);
        }

        [Fact]
        public void Place_WhenShipsOverlap_ShouldThrowInvalidOperationException()
        {
            var firstShip = BuildSampleHorizontalShip();

            var secondShip = new Ship
            {
                Length = 4,
                StartingPoint = new Point(1, 0),
                Orientation = ShipOrientation.VERTICAL
            };

            _board.Place(firstShip);

            Action act = () => _board.Place(secondShip);

            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Place_WhenShipCanBePlacedCorrectly_ShouldNotThrow()
        {
            var firstShip = BuildSampleHorizontalShip();

            var secondShip = new Ship
            {
                Length = 4,
                StartingPoint = new Point(3, 3),
                Orientation = ShipOrientation.VERTICAL
            };

            _board.Place(firstShip);

            Action act = () => _board.Place(secondShip);

            act.Should().NotThrow();
        }

        [Fact]
        public void Shoot_WhenShipIsHit_ShouldMarkPinCorrectly()
        {
            var ship = BuildSampleHorizontalShip();

            var shotX = 2;
            var shotY = 0;
            var shot = new Point(shotX, shotY);

            _board.Place(ship);

            _board.Shoot(shot);

            _board.Fields[shotX, shotY].State.Should().Be(FieldState.HIT);
        }

        [Fact]
        public void Shoot_WhenShipIsHitButNotDestroyed_ShouldReturnCorrectResult()
        {
            var ship = BuildSampleHorizontalShip();

            var shotX = 2;
            var shotY = 0;
            var shot = new Point(shotX, shotY);

            _board.Place(ship);

            var shootResult = _board.Shoot(shot);

            shootResult.State.Should().Be(FieldState.HIT);
            shootResult.HitShipName.Should().Be(ship.Name);
            shootResult.HitShipDestroyed.Should().BeFalse();
        }

        [Fact]
        public void Shoot_WhenShipIsHitAndDestroyed_ShouldReturnCorrectResult()
        {
            var ship = BuildSampleOneFieldShip();

            var shotX = 0;
            var shotY = 0;
            var shot = new Point(shotX, shotY);

            _board.Place(ship);

            var shootResult = _board.Shoot(shot);

            shootResult.State.Should().Be(FieldState.HIT);
            shootResult.HitShipName.Should().Be(ship.Name);
            shootResult.HitShipDestroyed.Should().BeTrue();
        }

        [Fact]
        public void Shoot_WhenShipIsMissed_ShouldMarkPinCorrectly()
        {
            var ship = BuildSampleHorizontalShip();

            var shotX = 1;
            var shotY = 2;
            var shot = new Point(shotX, shotY);

            _board.Place(ship);

            _board.Shoot(shot);

            _board.Fields[shotX, shotY].State.Should().Be(FieldState.MISS);
        }

        [Fact]
        public void Shoot_WhenShipIsMissed_ShouldReturnCorrectResult()
        {
            var ship = BuildSampleHorizontalShip();

            var shotX = 1;
            var shotY = 2;
            var shot = new Point(shotX, shotY);

            _board.Place(ship);

            var shootResult = _board.Shoot(shot);

            shootResult.State.Should().Be(FieldState.MISS);
            shootResult.HitShipName.Should().BeNullOrEmpty();
            shootResult.HitShipDestroyed.Should().BeFalse();
        }

        [Fact]
        public void CanPlace_WhenCanPlaceShip_ReturnTrue()
        {
            var firstShip = BuildSampleHorizontalShip();

            var secondShip = new Ship
            {
                Length = 4,
                StartingPoint = new Point(1, 1),
                Orientation = ShipOrientation.VERTICAL
            };

            _board.Place(firstShip);

            var canPlaceShip = _board.CanPlace(secondShip);

            canPlaceShip.Should().BeTrue();
        }

        [Fact]
        public void CanPlace_WhenShipsOverlap_ReturnFalse()
        {
            var firstShip = BuildSampleHorizontalShip();

            var secondShip = new Ship
            {
                Length = 4,
                StartingPoint = new Point(1, 0),
                Orientation = ShipOrientation.VERTICAL
            };

            _board.Place(firstShip);

            var canPlaceShip = _board.CanPlace(secondShip);

            canPlaceShip.Should().BeFalse();
        }

        private Ship BuildSampleVerticalShip()
        {
            return new Ship
            {
                Name = "Black Pearl",
                Length = 5,
                StartingPoint = new Point(5, 1),
                Orientation = ShipOrientation.VERTICAL
            };
        }

        private Ship BuildSampleHorizontalShip()
        {
            return new Ship
            {
                Name = "Titanic",
                Length = 4,
                StartingPoint = new Point(0, 0),
                Orientation = ShipOrientation.HORIZONTAL
            };
        }

        private Ship BuildSampleOneFieldShip()
        {
            return new Ship
            {
                Name = "Of Course I Still Love You",
                Length = 1,
                StartingPoint = new Point(0, 0),
                Orientation = ShipOrientation.HORIZONTAL
            };
        }
    }
}
