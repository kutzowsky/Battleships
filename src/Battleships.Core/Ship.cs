using Battleships.Core.Enums;
using Battleships.Core.Interfaces;
using System;
using System.Drawing;

namespace Battleships.Core
{
    public class Ship : IShip
    {
        public Guid Id { get; private set; }
        public ShipType Type { get; set; }
        public Point StartingPoint { get; set; }
        public byte Length { get; set; }

        public ShipOrientation Orientation { get; set; }

        public bool Destroyed { get { return Length == 0;  } }

        public Ship()
        {
            Id = Guid.NewGuid();
        }

        public void Hit()
        {
            if (Length > 0) Length--;
        }
    }
}
