using System.Drawing;

namespace Battleships.Core
{
    public class Board
    {
        public Field[,] Fields { get; private set; }

        public Board()
        {
            Fields = new Field[10,10];
        }

        public void Place(Ship ship)
        {
            var x = ship.StartingPoint.X;
            var y = ship.StartingPoint.Y;

            for (var i = 0; i < ship.Length; i++)
            {
                Fields[x, y] = Field.SHIP;

                if (ship.Orientation == ShipOrientation.HORIZONTAL)
                    x++;
                else
                    y++;
            }
        }

        public void Shoot(Point shot)
        {
            if (Fields[shot.X, shot.Y] == Field.SHIP)
                Fields[shot.X, shot.Y] = Field.HIT;
            else
                Fields[shot.X, shot.Y] = Field.MISS;
        }
    }
}
