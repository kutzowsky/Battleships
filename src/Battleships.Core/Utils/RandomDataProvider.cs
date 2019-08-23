using System;
using System.Drawing;
using Battleships.Core.Enums;
using Battleships.Core.Interfaces;

namespace Battleships.Core.Utils
{
    public class RandomDataProvider : IRandomDataProvider
    {
        public ShipOrientation GetRandomOrientation()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 2);

            return (ShipOrientation)randomNumber;
        }

        public Point GetRandomStartingPoint(ShipOrientation orientation, byte shipLength)
        {
            Random random = new Random();
            var coordinate = random.Next(0, 10);
            var lengthConstraintCoordinate = random.Next(0, 10 - shipLength);

            if (orientation == ShipOrientation.HORIZONTAL)
            {
                return new Point(lengthConstraintCoordinate, coordinate);
            }
            else
            {
                return new Point(coordinate, lengthConstraintCoordinate);
            }
        }
    }
}
