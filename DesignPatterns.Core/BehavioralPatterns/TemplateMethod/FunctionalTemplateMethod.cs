using System;

namespace DesignPatterns.Core.BehavioralPatterns.TemplateMethod
{
    public static class GameTemplate
    {
        public static void Run(
            Action start,
            Action takeTurn,
            Func<bool> haveWinner,
            Func<int> winningPlayer)
        {
            start();
            while (!haveWinner())
                takeTurn();
            Console.WriteLine($"Player {winningPlayer()} wins.");
        }
    }

    public class FunctionalTemplateMethodDemo
    {
        static void Main(string[] args)
        {
            var numberOfPlayers = 2;
            int currentPlayer = 0;
            int turn = 1, maxTurns = 10;

            void Start()
            {
                Console.WriteLine($"Starting a game of chess with {numberOfPlayers} players.");
            }

            bool HaveWinner()
            {
                return turn == maxTurns;
            }

            void TakeTurn()
            {
                Console.WriteLine($"Turn {turn++} taken by player {currentPlayer}.");
                currentPlayer = (currentPlayer + 1) % numberOfPlayers;
            }

            int WinningPlayer()
            {
                return currentPlayer;
            }

            GameTemplate.Run(Start, TakeTurn, HaveWinner, WinningPlayer);
        }
    }
}
