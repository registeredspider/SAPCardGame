using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Entities
{
    public class CardDeck
    {
        private readonly List<Card> _cards;

        public CardDeck()
        {
            _cards = new List<Card>();
        }

        public CardDeck(int capacity, int maxCardValue)
        {
            _cards = new List<Card>();
            InitializeCardList(capacity, maxCardValue);
        }

        public CardDeck(IEnumerable<Card> cards)
        {
            _cards = new List<Card>(cards);
        }

        public bool IsEmpty => _cards == null || !_cards.Any();

        public int TotalCardCount => _cards.Count();

        public void AddCard(Card card)
        {
            _cards.Add(card);
        }

        public void AddCards(IEnumerable<Card> cards)
        {
            _cards.AddRange(cards);
        }

        public Card GetCard(int index)
        {
            return _cards.ElementAt(index);
        }

        public List<Card> GetCards()
        {
            return new List<Card>(_cards);
        }

        public CardDeck GetRange(int start, int end)
        {
            return new CardDeck(_cards.GetRange(start, end));
        }

        public Card GetTopCard()
        {
            if (IsEmpty)
            {
                throw new Exception("Deck has no card left.");
            }
            int top = _cards.Count() - 1;
            Card card = _cards.ElementAt(top);
            _cards.RemoveAt(top);
            return card;
        }

        public void SetCard(int index, Card card)
        {
            _cards[index] = card;
        }

        public void Shuffle(IShuffleAlgo shuffleAlgo)
        {
            shuffleAlgo.Shuffle(this);
        }

        private void InitializeCardList(int capacity, int maxCardValue)
        {
            List<Card> coreDeck = Enumerable.Range(1, maxCardValue).Select(x => Card.ConvertToCard(x)).ToList();

            //Each card shows a number from minCardValue to maxCardValue.
            //Each number is in the deck four times for a total of 40 cards.
            int cardGroups = capacity / coreDeck.Count();

            for (int i = 0; i < cardGroups; i++)
            {
                _cards.AddRange(coreDeck);
            }
        }
    }
}