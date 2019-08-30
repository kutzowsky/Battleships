using Battleships.Core;
using Battleships.Core.Enums;
using Battleships.Core.Interfaces;
using Battleships.Core.Utils;
using Battleships.Core.Utils.Interfaces;
using Moq;
using Xunit;

namespace Battleships.Core.Tests.Utils
{
    public class FleetDeployerTests
    {
        IFleetDeployer  _fleetDeployer;

        readonly Mock<IShipGenerator> _shipGeneratorMock;
        readonly Mock<IRandomDataProvider> _randomDataProviderMock;

        readonly Mock<IBoard> _boardMock;

        public FleetDeployerTests()
        {
            _shipGeneratorMock = new Mock<IShipGenerator>();

            var battleship = new Ship
            {
                Type = ShipType.BATTLESHIP,
                Length = 5
            };

            var destroyer = new Ship
            {
                Type = ShipType.DESTROYER,
                Length = 4
            };

            _shipGeneratorMock.Setup(mock => mock.CreateBattleship()).Returns(battleship);
            _shipGeneratorMock.Setup(mock => mock.CreateDestroyer()).Returns(destroyer);

            _randomDataProviderMock = new Mock<IRandomDataProvider>();
            _fleetDeployer = new FleetDeployer(_shipGeneratorMock.Object, _randomDataProviderMock.Object);

            _boardMock = new Mock<IBoard>();
            _boardMock.Setup(mock => mock.CanPlace(It.IsAny<IShip>())).Returns(true);
        }

        [Fact]
        public void PlaceShipsOn_WhenCalled_ShouldUseShipGenerator()
        {
            _fleetDeployer.PlaceShipsOn(_boardMock.Object);

            _shipGeneratorMock.Verify(mock => mock.CreateBattleship(), Times.Once);
            _shipGeneratorMock.Verify(mock => mock.CreateDestroyer(), Times.Exactly(2));
        }

        [Fact]
        public void PlaceShipsOn_WhenCalled_ShouldUseGetRandomOrientationForAllShips()
        {
            _fleetDeployer.PlaceShipsOn(_boardMock.Object);

            _randomDataProviderMock.Verify(mock => mock.GetRandomOrientation(), Times.Exactly(3));
        }

        [Fact]
        public void PlaceShipsOn_WhenCalled_ShouldUseGetRandomStartingPointForBattleship()
        {
            _fleetDeployer.PlaceShipsOn(_boardMock.Object);

            _randomDataProviderMock.Verify(mock => mock.GetRandomStartingPoint(It.IsAny<ShipOrientation>(), 5), Times.Once);
        }

        [Fact]
        public void PlaceShipsOn_WhenCalled_ShouldUseGetRandomStartingPointForDestroyers()
        {
            _fleetDeployer.PlaceShipsOn(_boardMock.Object);

            _randomDataProviderMock.Verify(mock => mock.GetRandomStartingPoint(It.IsAny<ShipOrientation>(), 4), Times.Exactly(2));
        }


        [Fact]
        public void PlaceShipsOn_WhenCalled_ShouldPlaceThreeShipsOnTheBoard()
        {
            _fleetDeployer.PlaceShipsOn(_boardMock.Object);

            _boardMock.Verify(mock => mock.Place(It.IsAny<IShip>()), Times.Exactly(3));
        }

        [Fact]
        public void PlaceShipsOn_WhenCalled_CheckIfItCanPlaceSecondAndThirdShip()
        {
            _fleetDeployer.PlaceShipsOn(_boardMock.Object);

            _boardMock.Verify(mock => mock.CanPlace(It.IsAny<IShip>()), Times.Exactly(2));
        }
    }
}
