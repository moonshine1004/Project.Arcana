using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Arcana.Cards.Domain.Decks
{
    public sealed class Deck
    {
        private readonly List<CardDefinition> _cards;
        private readonly ReadOnlyCollection<CardDefinition> _readOnlyCards;

        public IReadOnlyCollection<CardDefinition> Cards => _readOnlyCards;

        public int Count => _cards.Count;
        
        public Deck(ReadOnlyCollection<CardDefinition> cards)
        {
            if (cards == null)
            {
                throw new ArgumentNullException(nameof(cards));
            }

            _cards = new List<CardDefinition>();

            foreach (CardDefinition card in cards)
            {
                if (card == null)
                {
                    throw new ArgumentException(
                        "Deck cannot contain a null card.",
                        nameof(cards));
                }
                _cards.Add(card);
            }

            _readOnlyCards = _cards.AsReadOnly();
        }
    }
}