﻿using Battleships.Core.Enums;
using Battleships.Core.Interfaces;
using System;

namespace Battleships.Core
{
    public class Field : IField
 
    {
        public Guid ShipId { get;  set; }
        public FieldState State { get; private set; }

        public Field() : this(FieldState.EMPTY) { }
        
        public Field(FieldState state)
        {
            State = state;
        }

        public void PlaceShip(Guid shipId)
        {
            State = FieldState.SHIP;
            ShipId = shipId;
        }

        public FieldState Shoot()
        {
            if (State == FieldState.HIT || State == FieldState.MISS) throw new InvalidOperationException("Field shot already");

            if (State == FieldState.SHIP) State = FieldState.HIT;
            else State = FieldState.MISS;

            return State;
        }
    }
}
