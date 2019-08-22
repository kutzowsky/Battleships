namespace Battleships.Core.Interfaces
{
    interface IGame
    {
        bool Active { get;}

        IBoard Board { get; }

        void Start();
        void Shoot(string coordinates);

        IField [,] GetBoardFields();
    }
}
