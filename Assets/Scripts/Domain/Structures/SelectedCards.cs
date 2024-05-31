using System;
using System.Threading.Tasks;
using Component.EventSystem;
using Domain.Constants;
using Presentation.Game.Card;
using UnityEngine;

namespace Domain.Structures
{
    [Serializable]
    public class SelectedCards
    {
        [SerializeField] private Card _firstCard;
        [SerializeField] private Card _secondCard;

        public SelectedCards(Card firstCard)
        {
            _firstCard = firstCard;
            _secondCard = null;
        }

        public bool IsAnySlotEmpty
        {
            get
            {
                if (_firstCard == null) return true;
                return _secondCard == null;
            }
        }

        public bool CanCheckCards => !_firstCard.IsFlipping || !_secondCard.IsFlipping;

        public bool AreCardsSame => _firstCard.Id == _secondCard.Id;

        public bool SelectCard(Card card)
        {
            if (_firstCard == null)
            {
                _firstCard = card;
                return true;
            }
            else if (_secondCard == null)
            {
                _secondCard = card;
                return true;
            }

            return false;
        }

        public async void SuccessSelect()
        {
            EventService.Invoke(GameEvents.ON_CARDS_CHECKED, true);
            await Task.Delay(500);
            _firstCard.Success();
            _secondCard.Success();
        }

        public async void FailSelect()
        {
            EventService.Invoke(GameEvents.ON_CARDS_CHECKED, false);
            await Task.Delay(500);
            _firstCard.Fail();
            _secondCard.Fail();
        }
    }
}