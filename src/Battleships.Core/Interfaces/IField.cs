using Battleships.Core.Enums;
using System;

namespace Battleships.Core.Interfaces
{
    public interface IField
    {
        Guid ShipId { get; set; }
        FieldState State { get; }

        void PlaceShip(Guid shipId);
        FieldState Shoot();
    }
}