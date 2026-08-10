using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Arcana.Cards.Domain.Decks
{
    public sealed class CardDeckState
    {
        // const
        public const int HandSize = 5;
        // feilds
        private readonly Random _random;
        private readonly CardDefinition[] _hand;
        private readonly List<CardDefinition> _drawPile; // 사용 전
        private readonly List<CardDefinition> _discardPile; // 사용 후
        private readonly ReadOnlyCollection<CardDefinition> _readOnlyHand;
        private readonly ReadOnlyCollection<CardDefinition> _readOnlyDrawPile;
        private readonly ReadOnlyCollection<CardDefinition> _readOnlyDiscardPile;
        // properties
        public IReadOnlyList<CardDefinition> Hand => _readOnlyHand;
        public IReadOnlyList<CardDefinition> DrawPile => _readOnlyDrawPile;
        public IReadOnlyList<CardDefinition> DiscardPile => _readOnlyDiscardPile;
        
        public CardDeckState(Deck deck, Random random)
        {
            if (deck == null)
            {
                throw new ArgumentNullException(nameof(deck));
            }
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            _random = random;

            _hand = new CardDefinition[HandSize];
            _drawPile = new List<CardDefinition>(deck.Cards);
            _discardPile = new List<CardDefinition>();

            _readOnlyHand = Array.AsReadOnly(_hand);
            _readOnlyDrawPile = _drawPile.AsReadOnly();
            _readOnlyDiscardPile = _discardPile.AsReadOnly();

            Shuffle(_drawPile);
            DrawInitialHand();
        }

        public CardDefinition GetHandCard(int handIndex)
        {
            if (handIndex < 0 || handIndex >= HandSize)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(handIndex),
                    handIndex,
                    $"handIndex는 {HandSize - 1} 사이");
            }
            return _hand[handIndex];
        }

        public CardDefinition UseCard(int handIndex)
        {
            if (handIndex < 0 || handIndex >= HandSize)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(handIndex),
                    handIndex,
                    $"handIndex는 {HandSize - 1} 사이");
            }

            CardDefinition usedCard = _hand[handIndex];

            _discardPile.Add(usedCard);
            _hand[handIndex] = null;

            if (_drawPile.Count == 0)
            {
                RefillDrawPile();
            }
            if (_drawPile.Count > 0)
            {
                DrawCardToHand(handIndex);
            }

            return usedCard;
        }

        private void DrawInitialHand()
        {
            for (int i = 0; i < HandSize; i++)
            {
                if (_drawPile.Count == 0)
                {
                    break;
                }
                DrawCardToHand(i);
            }
        }

        private void DrawCardToHand(int handIndex)
        {
            if (_drawPile.Count == 0)
            {
                return;
            }

            _hand[handIndex] = _drawPile[0];
            _drawPile.RemoveAt(0);
        }

        private void RefillDrawPile()
        {
            if (_discardPile.Count == 0)
            {
                return;
            }

            Shuffle(_discardPile);

            _drawPile.AddRange(_discardPile);
            _discardPile.Clear();
        }

        /// <summary>
        /// Fisher–Yates Shuffle
        /// </summary>
        private void Shuffle<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int randomIndex = _random.Next(i, list.Count);

                (list[i], list[randomIndex]) =
                    (list[randomIndex], list[i]);
            }
        }
    }
}