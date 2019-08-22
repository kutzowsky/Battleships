using System.Drawing;

namespace Battleships.Core
{
    public class Board
    {
        public Field[,] Fields { get; private set; }

        public Board()
        {
            Fields = new Field[10,10];

            for(var i=0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    Fields[i, j] = new Field();
                }
            }
        }

        public bool CanPlace(Ship ship)
        {
            var x = ship.StartingPoint.X;
            var y = ship.StartingPoint.Y;

            for (var i = 0; i < ship.Length; i++)
            {
                if (Fields[x, y].State == FieldState.SHIP) return false;

                if (ship.Orientation == ShipOrientation.HORIZONTAL)
                    x++;
                else
                    y++;
            }

            return true;
        }

        public void Place(Ship ship)
        {
            var x = ship.StartingPoint.X;
            var y = ship.StartingPoint.Y;

            for (var i = 0; i < ship.Length; i++)
            {
                Fields[x, y].PlaceShip(0);

                if (ship.Orientation == ShipOrientation.HORIZONTAL)
                    x++;
                else
                    y++;
            }
        }

        public void Shoot(Point shot)
        {
            Fields[shot.X, shot.Y].Shoot();
        }
    }
}
