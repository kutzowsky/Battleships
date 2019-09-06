using Battleships.Core;
using Battleships.Core.Enums;
using Battleships.Core.Interfaces;
using Battleships.Core.Models;
using Battleships.Core.Utils;
using System;

namespace BattleShips
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("BATTLESHIP GAME\n");

            var game = CreateGame();
            game.Start();

            DisplayLegend();
            Console.WriteLine();

            MainGameLoop(game);

            Console.WriteLine("Game over.");
            Console.ReadKey();
        }

        static Game CreateGame()
        {
            var board = new Board();
            var coordinateTranslator = new CoordinateTranslator();

            var shipGenerator = new ShipGenerator();
            var randomDataProvider = new RandomDataProvider();
            var fleetDeployer = new FleetDeployer(shipGenerator, randomDataProvider);

            return new Game(board, coordinateTranslator, fleetDeployer);
        }

        private static void DisplayLegend()
        {
            Console.WriteLine("Board symbols:");
            Console.WriteLine($"Unknown: {DisplaySettings.EMPTY_SYMBOL}");
            Console.WriteLine($"Hit: {DisplaySettings.HIT_SYMBOL}");
            Console.WriteLine($"Missed: {DisplaySettings.MISS_SYMBOL}");
        }

        private static void MainGameLoop(Game game)
        {
            while (game.Active)
            {
                DisplayBoard(game);
                Console.WriteLine();

                Console.WriteLine("Enter coordinates:");
                var coords = Console.ReadLine();

                ShotResult result = null;
                try
                {
                    result = game.Shoot(coords);
                }
                catch (Exception exc)
                {
                    if (exc is ArgumentException || exc is InvalidOperationException)
                    {
                        Console.WriteLine($"Error! {exc.Message} Try again.");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine($"Critical error! {exc.Message} Closing application.");
                        Environment.Exit(1);
                    }
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

                Console.WriteLine();
            }
        }

        private static void DisplayBoard(IGame game)
        {
            for (int i = 0; i < Settings.BOARD_SIZE; i++)
            {
                for (int j = 0; j < Settings.BOARD_SIZE; j++)
                {
                    var currentField = game.BoardFields[i, j];
                    char symbol = GetDisplaySymbol(currentField);

                    Console.Write($"{symbol} ");
                }

                Console.WriteLine();
            }
        }

        private static char GetDisplaySymbol(IField currentField)
        {
            char symbol;
            switch (currentField.State)
            {
                case FieldState.HIT:
                    symbol = DisplaySettings.HIT_SYMBOL;
                    break;
                case FieldState.MISS:
                    symbol = DisplaySettings.MISS_SYMBOL;
                    break;
                default:
                    symbol = DisplaySettings.EMPTY_SYMBOL;
                    break;
            }

            return symbol;
        }
    }
}
