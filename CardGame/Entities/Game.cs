using System;

namespace CardGame.Entities
{
    public class Game
    {
        private readonly CardDeck _cardDeck;
        private readonly Player _player1;
        private readonly Player _player2;
        private readonly IShuffleAlgo _shuffleAlgo;

        /// <summary>
        /// c'tor
        /// </summary>
        /// <param name="player1"> <see cref="Player"/> playing the game</param>
        /// <param name="player2"><see cref="Player"/> playing the game</param>
        /// <param name="shuffleAlgo"><see cref="IShuffleAlgo"/> Logic to shuffle card</param>
        public Game(Player player1, Player player2, IShuffleAlgo shuffleAlgo, int totalCards, int maxCardValue)
        {
            _player1 = player1;
            _player2 = player2;
            _shuffleAlgo = shuffleAlgo;
            _cardDeck = new CardDeck(totalCards, maxCardValue);
        }

        #region Private Methods

        /// <summary>
        ///  Return true : when one of the players has no card to draw either in draw pile or discard pile
        ///  else false
        /// </summary>
        private bool PlayerHasNoCardLeft
        {
            get
            {
                return _player1.NoCardLeft || _player2.NoCardLeft;
            }
        }

        /// <summary>
        /// Identifies winner based on the card facevalue comparision
        /// </summary>
        /// <param name="playerOneTurn"></param>
        /// <param name="playerTwoTurn"></param>
        /// <returns></returns>
        private Player IdentifyWinner(Card player1Card, Card player2Card)
        {
            return player1Card.CompareTo(player2Card) > 0 ? _player1 : _player2;
        }

        /// <summary>
        /// Performs
        /// 1. Card Shuffling
        /// 2. Equally distribute cards between the players
        /// </summary>
        private void PrepareCards()
        {
            //Shuffle cards
            _cardDeck.Shuffle(_shuffleAlgo);

            //Give first half to  player1
            _player1.SetDrawPile(_cardDeck.GetRange(0, _cardDeck.TotalCardCount / 2));

            //Give second half to player2
            _player2.SetDrawPile(_cardDeck.GetRange(_cardDeck.TotalCardCount / 2, _cardDeck.TotalCardCount - _cardDeck.TotalCardCount / 2));
        }
        private CardDeck StartRound(CardDeck discardPile)
        {
            var newRoundCardDeck = new CardDeck(discardPile.GetCards());

            Card playerOneCard = _player1.DrawCard();
            Console.WriteLine("Player 1 ({0} cards): {1}", _player1.TotalCard + 1, playerOneCard.FaceValue);

            Card playerTwoCard = _player2.DrawCard();
            Console.WriteLine("Player 2 ({0} cards): {1}", _player2.TotalCard + 1, playerTwoCard.FaceValue);

            newRoundCardDeck.AddCard(playerOneCard);
            newRoundCardDeck.AddCard(playerTwoCard);

            if (playerOneCard.CompareTo(playerTwoCard) != 0)
            {
                Player roundWinner = IdentifyWinner(playerOneCard, playerTwoCard);
                foreach (Card card in newRoundCardDeck.GetCards())
                {
                    roundWinner.AddCardToDiscardPile(card);
                }
                newRoundCardDeck = new CardDeck();
                Console.WriteLine("{0} wins this round", roundWinner.Name);
            }
            else
            {
                Console.WriteLine("No winner in this round");
            }
            return newRoundCardDeck;
        }

        #endregion Private Methods

        public void Start()
        {
            PrepareCards();

            var discardPile = new CardDeck();
            var tie = true;
            while (!PlayerHasNoCardLeft)
            {
                var oldDiscardPileCount = discardPile.TotalCardCount;
                discardPile = StartRound(discardPile);
                if (discardPile.TotalCardCount - oldDiscardPileCount != 2)
                {
                    tie = false;
                }
            }

            if (!tie)
            {
                var winner = _player1.NoCardLeft ? _player2 : _player1;
                Console.WriteLine("{0} wins the game!", winner.Name);
            }
            else
            {
                Console.WriteLine("It's tie between {0} & {1}!", _player1.Name, _player2.Name);
            }
        }
    }
}