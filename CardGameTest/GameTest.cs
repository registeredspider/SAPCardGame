using CardGame.Entities;
using Moq;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace CardGameTest
{
    public class GameTest
    {
        [Fact]
        public void Start_ShouldAnnounceTie_WhenThereIsATie()
        {
            //Arrange
            var shuffleAlgoMock = new Mock<IShuffleAlgo>();
            shuffleAlgoMock.Setup(x => x.Shuffle(It.IsAny<CardDeck>())).Callback((CardDeck deck) => { });
            var player1 = new Player("Player1", shuffleAlgoMock.Object);
            var player2 = new Player("Player2", shuffleAlgoMock.Object);
            var game = new Game(player1, player2, shuffleAlgoMock.Object, 2, 1);

            var output = new StringWriter();
            Console.SetOut(output);

            //Act
            game.Start();

            //Assert
            var outputString = output.ToString();
            var resultAnnouncement = outputString.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .ToList().Last();
            Assert.Contains($"It's tie between {player1.Name} & {player2.Name}", resultAnnouncement);
        }
    }
}