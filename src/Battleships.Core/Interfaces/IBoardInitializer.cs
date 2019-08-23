using Battleships.Core.Utils.Interfaces;

namespace Battleships.Core.Interfaces
{
    public interface IBoardInitializer
    {
        IShipGenerator ShipGenerator { get; }

        IRandomDataProvider RandomDataProvider { get; }

        void PlaceShipsOn(IBoard board);
    }
}
