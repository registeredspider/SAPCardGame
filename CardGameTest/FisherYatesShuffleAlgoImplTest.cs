using CardGame.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CardGameTest
{
    public class FisherYatesShuffleAlgoImplTest
    {
        private readonly FisherYatesShuffleAlgoImpl _shuffler = new FisherYatesShuffleAlgoImpl();

        [Fact]
        public void ConvertToCard_WhenDeckHasOneCard_ShouldReturnTheSameDeck()
        {
            //Arrange
            var cardList = new List<Card>
            {
                new Card(1)
            };
            var deck = new CardDeck(cardList);
            var deckJsonString = JsonConvert.SerializeObject(deck);

            //Act
            _shuffler.Shuffle(deck);

            //Assert
            Assert.Equal(deckJsonString, JsonConvert.SerializeObject(deck));
        }

        [Theory]
        [InlineData(1, 2, 3, 4)]
        [InlineData(1, 1, 1, 1)]
        [InlineData(1, 4, 1, 2)]
        [InlineData(2, 3, 2, 2)]
        public void Shuffle_WhenDeckHasMoreThanOneCard_ShouldReturnTheShuffledDeck(params int[] faceValues)
        {
            //Arrange
            var deck = new CardDeck(faceValues.Select(x => new Card(x)).ToList());
            var initialFaceValues = deck.GetCards().Select(x => x.FaceValue);

            //Act
            _shuffler.Shuffle(deck);

            //Assert
            var shuffledFaceValues = deck.GetCards().Select(x => x.FaceValue);

            // As cards are shuffled, keeping cards in order then comparing the facevalue will
            // be an easy option to confirm shuffling does not drop the cards or add any duplicate card
            // i.e. shuffle logic is working well
            Assert.True(string.Join(",", initialFaceValues.OrderBy(x => x).ToArray())
                        .Equals(string.Join(",", shuffledFaceValues.OrderBy(x => x).ToArray())));
        }
    }
}