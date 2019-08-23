using Battleships.Core.Interfaces;

namespace Battleships.Core.Utils.Interfaces
{
    public interface IShipGenerator
    {
        IShip CreateBattleship();
        IShip CreateDestroyer();
    }
}
