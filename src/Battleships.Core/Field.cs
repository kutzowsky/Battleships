﻿using System;

namespace Battleships.Core
{
    public class Field
    {
        public Guid ShipId { get;  set; }
        public FieldState State { get; private set; }

        public void PlaceShip(Guid shipId)
        {
            State = FieldState.SHIP;
            ShipId = shipId;
        }

        public FieldState Shoot()
        {
            if (State == FieldState.SHIP) State = FieldState.HIT;
            else State = FieldState.MISS;

            return State;
        }
    }
}
