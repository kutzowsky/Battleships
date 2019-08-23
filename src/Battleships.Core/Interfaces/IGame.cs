using Battleships.Core.Models;
using Battleships.Core.Utils.Interfaces;

namespace Battleships.Core.Interfaces
{
    public interface IGame
    {
        IBoard Board { get; }

        ICoordinateTranslator CoordinateTranslator { get; }

        bool Active { get; }

        IField[,] BoardFields { get; }

        void Start();

        ShootResult Shoot(string coordinates);

    }
}
