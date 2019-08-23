using Battleships.Core.Interfaces;
using Battleships.Core.Utils.Interfaces;

namespace Battleships.Core.Utils
{
    public class ShipGenerator : IShipGenerator
    {
        public IShip CreateBattleship()
        {
            return CreateShip("Battleship", 5);
        }

        public IShip CreateDestroyer()
        {
            return CreateShip("Destroyer", 4);
        }

        private IShip CreateShip(string name, byte length)
        {
            return  new Ship
            {
                Length = length,
                Name = name
            };
        }
    }
}
