using Battleships.Core;
using Battleships.Core.Enums;
using Battleships.Core.Interfaces;
using Battleships.Core.Models;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Drawing;
using Xunit;

namespace Battleships.Core.Tests
{
    public class GameTests
    {
        IGame _game;
        readonly Mock<IBoard> _boardMock;
        readonly Mock<ICoordinateTranslator> _coordinateTranslatorMock;
        readonly Mock<IFleetDeployer > _fleetDeployerMock;

        public GameTests()
        {
            _boardMock = new Mock<IBoard>();
            _coordinateTranslatorMock = new Mock<ICoordinateTranslator>();
            _fleetDeployerMock = new Mock<IFleetDeployer >();
            _game = new Game(_boardMock.Object , _coordinateTranslatorMock.Object, _fleetDeployerMock.Object);
        }

        [Fact]
        public void IsActive_WhenThereIsAtLeastOneAliveShip_ShouldReturnTrue()
        {
            var destroyedShipMock = new Mock<IShip>();
            destroyedShipMock.SetupGet(ship => ship.Destroyed).Returns(true);

            var aliveShipMock = new Mock<IShip>();
            aliveShipMock.SetupGet(ship => ship.Destroyed).Returns(false);

            var ships = new List<IShip>
            {
                destroyedShipMock.Object,
                aliveShipMock.Object
            };

            _boardMock.SetupGet(mock => mock.Ships).Returns(ships);

            _game.Active.Should().BeTrue();
        }

        [Fact]
        public void IsActive_WhenAllShipsAreDestroyed_ShouldReturnFalse()
        {
            var firstDestroyedShipMock = new Mock<IShip>();
            firstDestroyedShipMock.SetupGet(ship => ship.Destroyed).Returns(true);

            var secondDestroyedShipMock = new Mock<IShip>();
            secondDestroyedShipMock.SetupGet(ship => ship.Destroyed).Returns(true);

            var ships = new List<IShip>
            {
                firstDestroyedShipMock.Object,
                secondDestroyedShipMock.Object
            };

            _boardMock.SetupGet(mock => mock.Ships).Returns(ships);

            _game.Active.Should().BeFalse();
        }

        [Fact]
        public void BoardFields_WhenCalled_ShouldReturnBoardFields()
        {
            var expectedFields = new Field[Settings.BOARD_SIZE, Settings.BOARD_SIZE];
            _boardMock.SetupGet(mock => mock.Fields).Returns(expectedFields);

            _game.BoardFields.Should().BeEquivalentTo(expectedFields);
        }

        [Fact]
        public void Start_WhenCalled_ShoulduseFleetDeployerToPlaceShips()
        {
            _game.Start();

            _fleetDeployerMock.Verify(mock => mock.PlaceShipsOn(It.IsAny<IBoard>()), Times.Once());
        }

        [Fact]
        public void Shoot_WhenGameIsNotActive_ShouldThrowInvalidOperationException()
        {
            var destroyedShipMock = new Mock<IShip>();
            destroyedShipMock.SetupGet(ship => ship.Destroyed).Returns(true);

            var ships = new List<IShip>{ destroyedShipMock.Object };

            _boardMock.SetupGet(mock => mock.Ships).Returns(ships);

            Action act = () => _game.Shoot("A1");

            act.Should().Throw<InvalidOperationException>();
        }
        [Fact]
        public void Shoot_WhenGameIsActive_ShouldNotThrow()
        {
            var aliveShipMock = new Mock<IShip>();
            aliveShipMock.SetupGet(ship => ship.Destroyed).Returns(false);

            var ships = new List<IShip> { aliveShipMock.Object };

            _boardMock.SetupGet(mock => mock.Ships).Returns(ships);

            Action act = () => _game.Shoot("A1");

            act.Should().NotThrow();
        }

        [Fact]
        public void Shoot_WhenCalled_ShouldCallBoardShoot()
        {
            var aliveShipMock = new Mock<IShip>();
            aliveShipMock.SetupGet(ship => ship.Destroyed).Returns(false);

            var ships = new List<IShip> { aliveShipMock.Object };

            _boardMock.SetupGet(mock => mock.Ships).Returns(ships);

            _game.Shoot("A1");

            _boardMock.Verify(mock => mock.Shoot(It.IsAny<Point>()), Times.Once());
        }

        [Fact]
        public void Shoot_WhenCalled_ShouldReturnShootResultFromTheBoard()
        {
            var aliveShipMock = new Mock<IShip>();
            aliveShipMock.SetupGet(ship => ship.Destroyed).Returns(false);

            var ships = new List<IShip> { aliveShipMock.Object };

            _boardMock.SetupGet(mock => mock.Ships).Returns(ships);

            var expectedResult = new ShotResult
            {
                IsHit = true,
                HitShipDestroyed = true
            };
            _boardMock.Setup(mock => mock.Shoot(It.IsAny<Point>())).Returns(expectedResult);

            var result = _game.Shoot("A1");

            result.Should().Be(expectedResult);
        }
    }
}
