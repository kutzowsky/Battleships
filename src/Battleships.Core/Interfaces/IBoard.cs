using Battleships.Core.Models;
using System.Collections.Generic;
using System.Drawing;

namespace Battleships.Core.Interfaces
{
    public interface IBoard
    {
        IField[,] Fields { get; }
        ICollection<IShip> Ships { get; }

        bool CanPlace(IShip ship);
        void Place(IShip ship);
        ShootResult Shoot(Point shot);
    }
}
