using Battleships.Core.Enums;
using Battleships.Core.Interfaces;
using Battleships.Core.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Battleships.Core
{
    public class Board : IBoard
    {
        public IField[,] Fields { get; private set; }
        public ICollection<IShip> Ships { get; private set; }

        public Board(IField[,] fields, ICollection<IShip> ships)
        {
            Fields = fields;
            Ships = ships;
        }

        public Board() : this(new Field[10, 10], new List<IShip>())
        {
            for(var i=0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    Fields[i, j] = new Field();
                }
            }
        }

        public bool CanPlace(IShip ship)
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

        public void Place(IShip ship)
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

        public ShotResult Shoot(Point shot)
        {
            var field = Fields[shot.X, shot.Y];
            var fieldState = field.Shoot();

            var result = new ShotResult
            {
                IsHit = fieldState == FieldState.HIT
            };

            if (fieldState == FieldState.HIT)
            {
                var shipId = field.ShipId;
                var ship = Ships.Single(s => s.Id == shipId);

                ship.Hit();

                result.HitShipDestroyed = ship.Destroyed;
                result.HitShipType = ship.Type;
            }

            return result;
        }
    }
}
