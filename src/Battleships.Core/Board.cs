using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Battleships.Core
{
    public class Board
    {
        public Field[,] Fields { get; private set; }
        public List<Ship> Ships { get; private set; }

        public Board()
        {
            Fields = new Field[10,10];
            Ships = new List<Ship>();

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
            if (!CanPlace(ship)) throw new InvalidOperationException("This ship cannot be placed");

            Ships.Add(ship);

            var x = ship.StartingPoint.X;
            var y = ship.StartingPoint.Y;

            for (var i = 0; i < ship.Length; i++)
            {
                Fields[x, y].PlaceShip(ship.Id);

                if (ship.Orientation == ShipOrientation.HORIZONTAL)
                    x++;
                else
                    y++;
            }
        }

        public ShootResult Shoot(Point shot)
        {
            var field = Fields[shot.X, shot.Y];
            var fieldState = field.Shoot();

            var result = new ShootResult();
            result.State = fieldState;

            if (fieldState == FieldState.HIT)
            {
                var shipId = field.ShipId;
                var ship = Ships.Single(s => s.Id == shipId);

                ship.Hit();

                result.HitShipDestroyed = ship.Destroyed;
                result.HitShipName = ship.Name;
            }

            return result;
        }
    }
}
