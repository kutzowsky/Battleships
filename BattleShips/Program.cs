using Battleships.Core;
using Battleships.Core.Enums;
using Battleships.Core.Models;
using Battleships.Core.Utils;
using System;

namespace BattleShips
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Battleship game");
            var game = CreateGame();
            game.Start();
            Console.WriteLine("Ships placed. You can start playing.");

            while (game.Active)
            {
                Console.WriteLine("Enter coordinates:");
                var coords = Console.ReadLine();

                ShotResult result;
                try
                {
                    result = game.Shoot(coords);
                }
                catch (ArgumentException exc) 
                {
                    Console.WriteLine($"Error! {exc.Message} Try again.");
                    continue;
                }

                if (result.IsHit)
                {
                    Console.WriteLine($"Hit! Ship: {result.HitShipType}");

                    if (result.HitShipDestroyed) Console.WriteLine("Sunk!");
                }
                else
                {
                    Console.WriteLine("Miss");
                }
            }

            Console.WriteLine("Game over.");
            Console.ReadKey();
        }

        static Game CreateGame()
        {
            var board = new Board();
            var coordinateTranslator = new CoordinateTranslator();

            var shipGenerator = new ShipGenerator();
            var randomDataProvider = new RandomDataProvider();
            var boardInitializer = new BoardInitializer(shipGenerator, randomDataProvider);

            return new Game(board, coordinateTranslator, boardInitializer);
        }
    }
}
