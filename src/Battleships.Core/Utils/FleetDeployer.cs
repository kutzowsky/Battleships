using Battleships.Core.Interfaces;
using Battleships.Core.Utils.Interfaces;

namespace Battleships.Core.Utils
{
    public class FleetDeployer : IFleetDeployer 
    {
        public IShipGenerator ShipGenerator { get; }

        public IRandomDataProvider RandomDataProvider { get; }

        public FleetDeployer(IShipGenerator shipGenerator, IRandomDataProvider randomDataProvider)
        {
            ShipGenerator = shipGenerator;
            RandomDataProvider = randomDataProvider;
        }

        public void PlaceShipsOn(IBoard board)
        {
            var battleship = ShipGenerator.CreateBattleship();

            battleship.Orientation = RandomDataProvider.GetRandomOrientation();
            battleship.StartingPoint = RandomDataProvider.GetRandomStartingPoint(battleship.Orientation, battleship.Length);

            board.Place(battleship);

            var destroyersPlaced = 0;

            while (destroyersPlaced < Settings.DESTROYER_COUNT)
            {
                var destroyer = ShipGenerator.CreateDestroyer();

                destroyer.Orientation = RandomDataProvider.GetRandomOrientation();
                destroyer.StartingPoint = RandomDataProvider.GetRandomStartingPoint(destroyer.Orientation, destroyer.Length);

                if(board.CanPlace(destroyer))
                {
                    board.Place(destroyer);
                    destroyersPlaced++;
                }
            }
        }
    }
}
