using Battleships.Core.Models;
using System.Collections.Generic;
using System.Drawing;

namespace Battleships.Core.Interfaces
{
    public interface IBoard
    {
        IField[,] Fields { get; }
        ICollection<Ship> Ships { get; }

        bool CanPlace(Ship ship);
        void Place(Ship ship);
        ShootResult Shoot(Point shot);
    }
}
