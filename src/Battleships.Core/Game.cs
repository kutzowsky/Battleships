using Battleships.Core.Interfaces;

namespace Battleships.Core
{
    class Game : IGame
    {
        public bool Active { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public IBoard Board => throw new System.NotImplementedException();

        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void Shoot(string coordinates)
        {
            throw new System.NotImplementedException();
        }

        public IField[,] GetBoardFields()
        {
            throw new System.NotImplementedException();
        }
    }
}
