using CardGame.Entities;

namespace CardGame
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var shuffler = new FisherYatesShuffleAlgoImpl();

            var player1 = new Player("[Michael]", shuffler);
            var player2 = new Player("[Peter]", shuffler);

            var cardGame = new Game(player1, player2, shuffler, 40, 10);

            cardGame.Start();
        }
    }
}