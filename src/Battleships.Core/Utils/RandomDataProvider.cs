using System;
using System.Drawing;
using Battleships.Core.Enums;
using Battleships.Core.Interfaces;

namespace Battleships.Core.Utils
{
    public class RandomDataProvider : IRandomDataProvider
    {
        private static Random _random;

        public RandomDataProvider()
        {
            _random = new Random();
        }

        public ShipOrientation GetRandomOrientation()
        {
            int randomNumber = _random.Next(0, 2);

            return (ShipOrientation)randomNumber;
        }

        public Point GetRandomStartingPoint(ShipOrientation orientation, byte shipLength)
        {
            var coordinate = _random.Next(0, Settings.BOARD_SIZE);
            var lengthConstraintCoordinate = _random.Next(0, Settings.BOARD_SIZE - shipLength);

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
