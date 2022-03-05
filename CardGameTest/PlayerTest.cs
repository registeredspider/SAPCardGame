using CardGame.Entities;
using System;
using Xunit;

namespace CardGameTest
{
    public class PlayerTest
    {
        [Fact]
        public void DrawCard_ShouldGetTopCardFromDrawPile_WhenDrawPileIsNotEmpty()
        {
            //Arrange
            var shuffleAlgo = new FisherYatesShuffleAlgoImpl();
            var player = new Player("Test Player", shuffleAlgo);
            var drawPile = new CardDeck();
            drawPile.AddCard(new Card(1));
            drawPile.AddCard(new Card(2));
            player.SetDrawPile(drawPile);

            //Act
            var drawnCard = player.DrawCard();

            //Assert
            Assert.Equal(2, drawnCard.FaceValue);
        }

        [Fact]
        public void DrawCard_ShouldFlipDiscardPileToDrawPile_WhenDrawPileEmptyAndDiscardPileIsNotEmpty()
        {
            //Arrange
            var shuffleAlgo = new FisherYatesShuffleAlgoImpl();
            var player = new Player("Test Player", shuffleAlgo);
            player.AddCardToDiscardPile(new Card(1));
            player.AddCardToDiscardPile(new Card(12));

            //Act
            var drawnCard = player.DrawCard();

            //Assert
            Assert.True(drawnCard.FaceValue == 12 || drawnCard.FaceValue == 1);
        }

        [Fact]
        public void DrawCard_ShouldThrowException_WhenDrawPileAndDiscardPileBothAreEmpty()
        {
            //Arrange
            var shuffleAlgo = new FisherYatesShuffleAlgoImpl();
            var player = new Player("Test Player", shuffleAlgo);

            //Assert
            Assert.Throws<Exception>(() => player.DrawCard()).Message.Equals("Cannot provide a card from an empty pile");

        }

        [Fact]
        public void TotalCard_ShouldCountCardsFromDrawAndDiscardPile()
        {
            //Arrange
            var shuffleAlgo = new FisherYatesShuffleAlgoImpl();
            var player = new Player("Test Player", shuffleAlgo);
            var drawPile = new CardDeck();
            drawPile.AddCard(new Card(1));
            drawPile.AddCard(new Card(2));
            player.SetDrawPile(drawPile);
            player.AddCardToDiscardPile(new Card(1));
            player.AddCardToDiscardPile(new Card(12));

            //Act
            var totalCards = player.TotalCard;

            //Assert
            Assert.Equal(4, totalCards);
        }
    }
}