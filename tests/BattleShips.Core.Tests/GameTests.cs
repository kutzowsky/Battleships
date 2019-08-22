using Battleships.Core;
using Battleships.Core.Interfaces;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace BattleShips.Core.Tests
{
    public class GameTests
    {
        IGame _game;
        List<IShip> _ships = new List<IShip>();

        public GameTests()
        {
            var fields = new Field[10, 10];
            _ships = new List<IShip>();

            var board = new Board(fields, _ships);
            _game = new Game(board);
        }

        [Fact]
        public void IsActive_WhenThereIsAtLeastOneAliveShip_ShouldReturnTrue()
        {
            var aliveShipMock = new Mock<IShip>();
            aliveShipMock.SetupGet(ship => ship.Destroyed).Returns(false);

            var destroyedShipMock = new Mock<IShip>();
            destroyedShipMock.SetupGet(ship => ship.Destroyed).Returns(true);

            _ships.Add(aliveShipMock.Object);
            _ships.Add(destroyedShipMock.Object);

            _game.Active.Should().BeTrue();
        }

        [Fact]
        public void IsActive_WhenAllShipsAreDestroyed_ShouldReturnFalse()
        {
            var firstDestroyedShipMock = new Mock<IShip>();
            firstDestroyedShipMock.SetupGet(ship => ship.Destroyed).Returns(true);

            var secondDestroyedShipMock = new Mock<IShip>();
            secondDestroyedShipMock.SetupGet(ship => ship.Destroyed).Returns(true);

            _ships.Add(firstDestroyedShipMock.Object);
            _ships.Add(secondDestroyedShipMock.Object);

            _game.Active.Should().BeFalse();
        }
    }
}
