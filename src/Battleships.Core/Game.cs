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

        public IBoardInitializer BoardInitializer { get; }

        public Game(IBoard board, ICoordinateTranslator coordinateTranslator, IBoardInitializer boardInitializer)
        {
            Board = board;
            CoordinateTranslator = coordinateTranslator;
            BoardInitializer = boardInitializer;
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
            BoardInitializer.PlaceShipsOn(Board);
        }

        public ShootResult Shoot(string coordinates)
        {
            if (!Active) throw new InvalidOperationException("Game is not active");

            var shootPoint = CoordinateTranslator.GetBoardCoordsFrom(coordinates);
            return Board.Shoot(shootPoint);
        }
    }
}
