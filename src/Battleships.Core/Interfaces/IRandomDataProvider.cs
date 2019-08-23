using Battleships.Core.Enums;
using System.Drawing;

namespace Battleships.Core.Interfaces
{
    public interface IRandomDataProvider
    {
        ShipOrientation GetRandomOrientation();
        Point GetRandomStartingPoint(ShipOrientation orientation, byte shipLength);
    }
}
