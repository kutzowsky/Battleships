using System.Drawing;

namespace Battleships.Core.Interfaces
{
    public interface ICoordinateTranslator
    {
        Point GetBoardCoordsFrom(string coordinates);
    }
}
