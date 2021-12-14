using CardGame.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CardGameTest
{
    public class CardDeckTest
    {
        [Theory]
        [InlineData(2, 1, 1, 1)]
        [InlineData(4, 2, 1, 2, 1, 2)]
        [InlineData(6, 3, 1, 2, 3, 1, 2, 3)]
        public void Ctor_WhenCapacityAndMaxCardValueIsSupplied_ShouldCreateCardDeckWithDefinedNumberOfCards(int capacity, int cardMaxFaceValue, params int[] expectedCards)
        {
            //Act
            var deck = new CardDeck(capacity, cardMaxFaceValue);

            //Assert
            var cardFaceValue = string.Join(",", deck.GetCards().Select(x => x.FaceValue).OrderBy(x => x));
            var expectedCardFaceValues = string.Join(",", expectedCards.OrderBy(x => x));
        }

        [Fact]
        public void Shuffle_ShouldUseShuffleAlgo()
        {
            //Arrange
            var deck = new CardDeck();
            var shuffleMock = new Mock<IShuffleAlgo>();
            shuffleMock.Setup(x => x.Shuffle(It.IsAny<CardDeck>()));

            //Act
            deck.Shuffle(shuffleMock.Object);

            //Assert
            shuffleMock.Verify(x => x.Shuffle(It.IsAny<CardDeck>()), Times.Once);
        }

        [Theory]
        [InlineData(4, 1, 2, 3, 4)]
        [InlineData(2, 1, 6)]
        public void TotalCardCount_ShouldShowCorrectCardCount(int expectedCount, params int[] cardFaceValue)
        {
            //Arrange
            var cards = cardFaceValue.Select(x => new Card(x));
            var deck = new CardDeck(cards);

            //Act
            var totalCardCount = deck.TotalCardCount;

            //Assert
            Assert.Equal(expectedCount, totalCardCount);
        }

        [Theory]
        [InlineData(3, 1, 2, 3, 4)]
        [InlineData(5, 1, 6, 5, 8, 8)]
        public void GetCard_ShouldRetrieveTheCorrectCardFromPosition(int expectedFaceValue, int index, params int[] cardFaceValue)
        {
            //Arrange
            var cards = cardFaceValue.Select(x => new Card(x));
            var deck = new CardDeck(cards);

            //Act
            var card = deck.GetCard(index);

            //Assert
            Assert.Equal(expectedFaceValue, card.FaceValue);
        }

        [Theory]
        [InlineData(2, 1, 2, 3, 4)]
        [InlineData(6, 1, 6, 5, 8, 8)]
        public void SetCard_ShouldCardShouldBeInsertedAtGivenPosition(int expectedFaceValue, int index, int newCardFaceValue, params int[] cardFaceValue)
        {
            //Arrange
            var cards = cardFaceValue.Select(x => new Card(x));
            var deck = new CardDeck(cards);
            var newCard = new Card(newCardFaceValue);
            //Act
            deck.SetCard(index, newCard);

            //Assert
            var cardAtPosition = deck.GetCard(index);
            Assert.Equal(expectedFaceValue, cardAtPosition.FaceValue);
        }

        [Fact]
        public void IsEmpty_ShouldReturnEmpty_WhenADeckHasNoCards()
        {
            //Arrange
            var deck = new CardDeck();

            //Act
            var isEmpty = deck.IsEmpty;

            //Assert
            Assert.True(isEmpty);
        }

        [Theory]
        [InlineData(2, 1, 2, 3, 4)]
        [InlineData(6, 1, 6, 5, 8, 8)]
        public void IsEmpty_ShouldNoReturnEmpty_WhenADeckHasSomeCards(params int[] cardFaceValue)
        {
            //Arrange
            var cards = cardFaceValue.Select(x => new Card(x));
            var deck = new CardDeck(cards);

            //Act
            var isEmpty = deck.IsEmpty;

            //Assert
            Assert.False(isEmpty);
        }

        [Theory]
        [InlineData(5, 1, 1, 2, 3, 4)]
        [InlineData(4, 1, 1, 2, 3)]
        public void AddCard(int expectedCount, int cardFaceValue, params int[] newCardFacevalues)
        {
            //arrange
            var cardDeck = new CardDeck(new List<Card> { new Card(cardFaceValue) });

            //Act
            cardDeck.AddCards(newCardFacevalues.Select(x => new Card(x)).ToList());

            //Assert
            Assert.Equal(expectedCount, cardDeck.TotalCardCount);
        }

        [Theory]
        [InlineData(1, 2, 2, 1, 2, 3, 4)]
        [InlineData(1, 2, 1, 1, 1, 2, 3)]
        public void GetRange_ShouldNoReturnCardDeckDesiredCards(int index, int count, params int[] cardFaceValue)
        {
            //Arrange
            var cards = cardFaceValue.Select(x => new Card(x));
            var deck = new CardDeck(cards);

            //Act
            var newDeck = deck.GetRange(index, count);

            //Assert
            var expectedFaceValues = string.Join(",", cardFaceValue.ToList().GetRange(index, count));
            var resultFaceValues = string.Join(",", newDeck.GetCards().Select(x => x.FaceValue));
            Assert.Equal(expectedFaceValues, resultFaceValues);
        }

        [Fact]
        public void GetTopCard_ShouldThrowException_WhenDeckIsEmpty()
        {
            //Arrange
            var cardDeck = new CardDeck();
            //Act
            Assert.Throws<Exception>(() => cardDeck.GetTopCard()).Message.Equals("Deck has no card left.");
        }

        [Theory]
        [InlineData(5, 1, 2, 3, 4, 5)]
        [InlineData(6, 1, 2, 4, 3, 4, 6)]
        public void GetTopCard_ShouldReturnTopCard_WhenDeckIsNotEmpty(int expectedTopCardFaceValue, params int[] cards)
        {
            //Arrange
            var cardDeck = new CardDeck();
            cardDeck.AddCards(cards.Select(x => new Card(x)));

            //Act
            var topCard = cardDeck.GetTopCard();

            Assert.Equal(expectedTopCardFaceValue, topCard.FaceValue);
        }
    }
}