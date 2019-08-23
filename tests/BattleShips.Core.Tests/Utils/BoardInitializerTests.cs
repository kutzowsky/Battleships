﻿using Battleships.Core;
using Battleships.Core.Enums;
using Battleships.Core.Interfaces;
using Battleships.Core.Utils;
using Battleships.Core.Utils.Interfaces;
using Moq;
using Xunit;

namespace BattleShips.Core.Tests.Utils
{
    public class BoardInitializerTests
    {
        IBoardInitializer _boardInitializer;

        readonly Mock<IShipGenerator> _shipGeneratorMock;
        readonly Mock<IRandomDataProvider> _randomDataProviderMock;

        readonly Mock<IBoard> _boardMock;

        public BoardInitializerTests()
        {
            _shipGeneratorMock = new Mock<IShipGenerator>();

            var battleship = new Ship
            {
                Name = "Battleship",
                Length = 5
            };

            var destroyer = new Ship
            {
                Name = "Destroyer",
                Length = 4
            };

            _shipGeneratorMock.Setup(mock => mock.CreateBattleship()).Returns(battleship);
            _shipGeneratorMock.Setup(mock => mock.CreateDestroyer()).Returns(destroyer);

            _randomDataProviderMock = new Mock<IRandomDataProvider>();
            _boardInitializer = new BoardInitializer(_shipGeneratorMock.Object, _randomDataProviderMock.Object);

            _boardMock = new Mock<IBoard>();
            _boardMock.Setup(mock => mock.CanPlace(It.IsAny<IShip>())).Returns(true);
        }

        [Fact]
        public void PlaceShipsOn_WhenCalled_ShouldUseShipGenerator()
        {
            _boardInitializer.PlaceShipsOn(_boardMock.Object);

            _shipGeneratorMock.Verify(mock => mock.CreateBattleship(), Times.Once);
            _shipGeneratorMock.Verify(mock => mock.CreateDestroyer(), Times.Exactly(2));
        }

        [Fact]
        public void PlaceShipsOn_WhenCalled_ShouldUseGetRandomOrientationForAllShips()
        {
            _boardInitializer.PlaceShipsOn(_boardMock.Object);

            _randomDataProviderMock.Verify(mock => mock.GetRandomOrientation(), Times.Exactly(3));
        }

        [Fact]
        public void PlaceShipsOn_WhenCalled_ShouldUseGetRandomStartingPointForBattleship()
        {
            _boardInitializer.PlaceShipsOn(_boardMock.Object);

            _randomDataProviderMock.Verify(mock => mock.GetRandomStartingPoint(It.IsAny<ShipOrientation>(), 5), Times.Once);
        }

        [Fact]
        public void PlaceShipsOn_WhenCalled_ShouldUseGetRandomStartingPointForDestroyers()
        {
            _boardInitializer.PlaceShipsOn(_boardMock.Object);

            _randomDataProviderMock.Verify(mock => mock.GetRandomStartingPoint(It.IsAny<ShipOrientation>(), 4), Times.Exactly(2));
        }


        [Fact]
        public void PlaceShipsOn_WhenCalled_ShouldPlaceThreeShipsOnTheBoard()
        {
            _boardInitializer.PlaceShipsOn(_boardMock.Object);

            _boardMock.Verify(mock => mock.Place(It.IsAny<IShip>()), Times.Exactly(3));
        }

        [Fact]
        public void PlaceShipsOn_WhenCalled_CheckIfItCanPlaceSecondAndThirdShip()
        {
            _boardInitializer.PlaceShipsOn(_boardMock.Object);

            _boardMock.Verify(mock => mock.CanPlace(It.IsAny<IShip>()), Times.Exactly(2));
        }
    }
}
