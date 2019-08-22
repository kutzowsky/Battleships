using System;
using System.Drawing;

namespace Battleships.Core
{
    public class Ship
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public Point StartingPoint { get; set; }
        public byte Length { get; set; }

        public ShipOrientation Orientation { get; set; }

        public Ship()
        {
            Id = Guid.NewGuid();
        }
    }
}
