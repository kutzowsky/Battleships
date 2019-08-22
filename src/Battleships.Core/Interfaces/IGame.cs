﻿using Battleships.Core.Models;

namespace Battleships.Core.Interfaces
{
    public interface IGame
    {
        IBoard Board { get; }

        bool Active { get; }

        IField[,] BoardFields { get; }

        void Start();

        ShootResult Shoot(string coordinates);

    }
}
