using System;
using System.Drawing;
using Battleships.Core.Enums;

namespace Battleships.Core.Interfaces
{
    public interface IShip
    {
        bool Destroyed { get; }
        Guid Id { get; }
        byte Length { get; set; }
        ShipType Type { get; set; }
        ShipOrientation Orientation { get; set; }
        Point StartingPoint { get; set; }

        void Hit();
    }
}