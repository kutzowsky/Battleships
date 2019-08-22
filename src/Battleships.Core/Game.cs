using Battleships.Core.Interfaces;
using System.Linq;

namespace Battleships.Core
{
    public class Game : IGame
    {
        public IBoard Board { get; } 

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

        public Game(IBoard board)
        {
            Board = board;
        }

        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void Shoot(string coordinates)
        {
            throw new System.NotImplementedException();
        }
    }
}
