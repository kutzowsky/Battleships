using System;
using System.Drawing;
using Battleships.Core.Interfaces;

namespace Battleships.Core.Utils
{
    public class CoordinateTranslator : ICoordinateTranslator
    {
        private const string letters = "ABCDEFGHIJ";

        public Point GetBoardCoordsFrom(string coordinates)
        {
            if (coordinates.Length != 2 && coordinates.Length != 3) throw new ArgumentException("Wrong coordinate length. Should be e. g.: A1.");

            coordinates = coordinates.ToUpper();

            var x = letters.IndexOf(coordinates[0]);
            if (x == -1) throw new ArgumentException($"Wrong X coordinate, should be one of: {letters}.");

            if (!int.TryParse(coordinates.Substring(1), out int y))
                throw new ArgumentException($"Wrong Y coordinate, should be numeric.");

            y--;
            if (y < 0 || y > 9) throw new ArgumentException($"Wrong Y coordinate. Should be from 1 to 10.");

            return new Point(x, y);
        }
    }
}
