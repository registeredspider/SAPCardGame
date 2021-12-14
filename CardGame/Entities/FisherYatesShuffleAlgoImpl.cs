using System;

namespace CardGame.Entities
{
    public class FisherYatesShuffleAlgoImpl : IShuffleAlgo
    {
        public void Shuffle(CardDeck cardDeck)
        {
            var random = new Random();
            for (int i = cardDeck.TotalCardCount - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                Card temp = cardDeck.GetCard(i);
                Card cardToMove = cardDeck.GetCard(j);
                cardDeck.SetCard(i, cardToMove);
                cardDeck.SetCard(j, temp);
            }
        }
    }
}