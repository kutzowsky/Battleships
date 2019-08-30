using Battleships.Core.Models;
using Battleships.Core.Utils.Interfaces;

namespace Battleships.Core.Interfaces
{
    public interface IGame
    {
        IBoard Board { get; }
        ICoordinateTranslator CoordinateTranslator { get; }
        IFleetDeployer FleetDeployer { get; }
        IField[,] BoardFields { get; }

        bool Active { get; }

        void Start();
        ShotResult Shoot(string coordinates);

    }
}
