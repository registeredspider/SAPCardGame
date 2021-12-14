using CardGame.Entities;

namespace CardGame
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var shuffler = new FisherYatesShuffleAlgoImpl();

            var player1 = new Player("Player 1", shuffler);
            var player2 = new Player("Player 2", shuffler);

            var cardGame = new Game(player1, player2, shuffler, 40, 10);

            cardGame.Start();
        }
    }
}