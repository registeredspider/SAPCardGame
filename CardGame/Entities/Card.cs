using System;

namespace CardGame.Entities
{
    public class Card : IComparable<Card>
    {
        private readonly int _faceValue;

        public Card(int value)
        {
            _faceValue = value;
        }

        public int FaceValue
        { get { return _faceValue; } }
        public static Card ConvertToCard(int faceValue)
        {
            return new Card(faceValue);
        }

        public int CompareTo(Card obj)
        {
            return _faceValue.CompareTo(obj._faceValue);
        }
    }
}