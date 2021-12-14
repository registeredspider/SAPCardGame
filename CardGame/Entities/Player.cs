using System;
using System.Collections.Generic;

namespace CardGame.Entities
{
    public class Player
    {
        private readonly string name;
        private readonly IShuffleAlgo shuffleAlgo;
        private CardDeck discardPile = new CardDeck();
        private CardDeck drawPile = new CardDeck();
        /// <summary>
        /// c'tor
        /// </summary>
        /// <param name="name"> <see cref="string"/> name of player</param>
        /// <param name="shuffleAlgo"> <see cref="IShuffleAlgo"/>Algo to be used for shuffling the cards</param>
        public Player(string name, IShuffleAlgo shuffleAlgo)
        {
            this.name = name;
            this.shuffleAlgo = shuffleAlgo;
        }

        public string Name
        { get { return name; } }
        #region Private Methods

        private void SwitchToDiscardPile()
        {
            discardPile.Shuffle(shuffleAlgo);
            drawPile = new CardDeck(new List<Card>(discardPile.GetCards()));
            discardPile = new CardDeck();
        }

        #endregion Private Methods

        public bool NoCardLeft => drawPile.IsEmpty && discardPile.IsEmpty;

        public int TotalCard
        {
            get { return drawPile.TotalCardCount + discardPile.TotalCardCount; }
        }

        public void AddCardToDiscardPile(Card card)
        {
            discardPile.AddCard(card);
        }

        public Card DrawCard()
        {
            // Fetch card from draw pile until it gets empty
            if (!drawPile.IsEmpty)
            {
                return drawPile.GetTopCard();
            }
            else if (!discardPile.IsEmpty)  // start using discard pile as draw Pile when draw pile goes empty and discard pile has some cards left
            {
                Console.WriteLine($"....{name} is switching to discard pile....");
                SwitchToDiscardPile();
                return drawPile.GetTopCard();
            }
            else
            {
                throw new Exception("Cannot provide a card from an empty pile");
            }
        }

        public void SetDrawPile(CardDeck cardDeck)
        {
            drawPile = cardDeck;
        }
    }
}