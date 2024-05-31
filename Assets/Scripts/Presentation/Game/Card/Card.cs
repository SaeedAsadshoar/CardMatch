using System;
using Component.EventSystem;
using Domain.Constants;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Presentation.Game.Card
{
    public class Card : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private CardTheme _cardTheme;
        [SerializeField] private CardAnimation _cardAnimation;

        private string _id;
        private Action<Card> _onClickOnCard;

        public bool IsFlipping => _cardAnimation.IsFlipping;
        public string Id => _id;

        private void OnEnable()
        {
            EventService.Subscribe<bool>(GameEvents.ON_HIDE_ALL_CARDS, OnHideAllCards);
        }

        private void OnDisable()
        {
            EventService.Unsubscribe<bool>(GameEvents.ON_HIDE_ALL_CARDS, OnHideAllCards);
        }

        private void OnHideAllCards(bool result)
        {
            HideCard();
        }

        public void Initialize(Texture2D theme, Texture2D card, float x, float y, Action<Card> onClickOnCard)
        {
            _onClickOnCard = onClickOnCard;
            transform.position = new Vector3(x, 0, y);
            _cardTheme?.LoadTheme(theme, card);

            _id = card.name;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _onClickOnCard?.Invoke(this);
            ShowCard();
        }

        private void ShowCard()
        {
            _cardAnimation.ShowCard();
        }

        private void HideCard()
        {
            _cardAnimation.HideCard();
        }

        public void Success()
        {
            _cardAnimation.Success();
        }

        public void Fail()
        {
            HideCard();
        }
    }
}