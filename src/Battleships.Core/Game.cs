using Battleships.Core.Interfaces;
using Battleships.Core.Models;
using System;
using System.Linq;

namespace Battleships.Core
{
    public class Game : IGame
    {
        public IBoard Board { get; }
        public ICoordinateTranslator CoordinateTranslator { get; }

        public IFleetDeployer  FleetDeployer { get; }

        public Game(IBoard board, ICoordinateTranslator coordinateTranslator, IFleetDeployer  fleetDeployer)
        {
            Board = board;
            CoordinateTranslator = coordinateTranslator;
            FleetDeployer = fleetDeployer;
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
            FleetDeployer.PlaceShipsOn(Board);
        }

        public ShotResult Shoot(string coordinates)
        {
            if (!Active) throw new InvalidOperationException("Game is not active");

            var shootPoint = CoordinateTranslator.GetBoardCoordsFrom(coordinates);
            return Board.Shoot(shootPoint);
        }
    }
}
