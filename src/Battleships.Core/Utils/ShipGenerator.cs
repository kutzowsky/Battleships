using Battleships.Core.Enums;
using Battleships.Core.Interfaces;
using Battleships.Core.Utils.Interfaces;

namespace Battleships.Core.Utils
{
    public class ShipGenerator : IShipGenerator
    {
        public IShip CreateBattleship()
        {
            return CreateShip(ShipType.BATTLESHIP, 5);
        }

        public IShip CreateDestroyer()
        {
            return CreateShip(ShipType.DESTROYER, 4);
        }

        private IShip CreateShip(ShipType type, byte length)
        {
            return  new Ship
            {
                Length = length,
                Type = type
            };
        }
    }
}
