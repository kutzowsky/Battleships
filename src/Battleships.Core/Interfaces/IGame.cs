namespace Battleships.Core.Interfaces
{
    public interface IGame
    {
        IBoard Board { get; }

        bool Active { get; }

        IField[,] BoardFields { get; }

        void Start();

        void Shoot(string coordinates);

    }
}
