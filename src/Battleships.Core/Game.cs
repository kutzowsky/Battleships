using Battleships.Core.Interfaces;
using Battleships.Core.Models;
using System;
using System.Drawing;
using System.Linq;

namespace Battleships.Core
{
    public class Game : IGame
    {
        public IBoard Board { get; }
        public ICoordinateTranslator CoordinateTranslator { get; }

        public Game(IBoard board, ICoordinateTranslator coordinateTranslator)
        {
            Board = board;
            CoordinateTranslator = coordinateTranslator;
        }

        public bool Active {
            get
            {
                return Board.Ships.Any(ship => ship.Destroyed == false);
            }
        }

        public IField[,] BoardFields
        {
            get
            {
                return Board.Fields;
            }
        }

        public void Start()
        {
            var ship = new Ship
            {
                Name = "Just testing",
                Length = 4,
                Orientation = Enums.ShipOrientation.HORIZONTAL,
                StartingPoint = new Point(0, 0)
            };

            if (Board.CanPlace(ship)) Board.Place(ship);
        }

        public ShootResult Shoot(string coordinates)
        {
            if (!Active) throw new InvalidOperationException("Game is not active");

            var shootPoint = CoordinateTranslator.GetBoardCoordsFrom(coordinates);
            return Board.Shoot(shootPoint);
        }
    }
}
