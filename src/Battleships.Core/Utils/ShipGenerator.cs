using Battleships.Core.Enums;
using Battleships.Core.Interfaces;
using Battleships.Core.Utils.Interfaces;

namespace Battleships.Core.Utils
{
    public class ShipGenerator : IShipGenerator
    {
        public IShip CreateBattleship()
        {
            return CreateShip(ShipType.BATTLESHIP, Settings.BATTLESHIP_LENGTH);
        }

        public IShip CreateDestroyer()
        {
            return CreateShip(ShipType.DESTROYER, Settings.DESTROYER_LENGTH);
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
