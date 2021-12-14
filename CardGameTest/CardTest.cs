using CardGame.Entities;
using Xunit;

namespace CardGameTest
{
    public class CardTest
    {
        [Fact]
        public void ConvertToCard_ShouldGenerateCardWithGivenFacevalue()
        {
            //Arrange
            var faceValue = 5;

            //Act
            var card = Card.ConvertToCard(5);

            //Assert
            Assert.Equal(faceValue, card.FaceValue);
        }

        [Theory]
        [InlineData(1, 1, 0)]
        [InlineData(2, 3, -1)]
        [InlineData(3, 2, 1)]
        public void CompareTo_ShouldGiveRightDifferenceValueBetweenCards(int card1FaceValue, int card2FaceValue, int expectedValue)
        {
            //Arrange
            var card1 = new Card(card1FaceValue);
            var card2 = new Card(card2FaceValue);

            //Act
            var result = card1.CompareTo(card2);

            //Assert
            Assert.Equal(expectedValue, result);
        }
    }
}