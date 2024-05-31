using System.Collections.Generic;
using Component.EventSystem;
using Component.PoolSystem;
using Component.ThemeSystem;
using Domain.Constants;
using Presentation.UI.Panels.Abstraction;
using Presentation.UI.Panels.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.UI.Panels.MainMenu
{
    public class ThemePanel : UIPanel
    {
        [SerializeField] private MainMenuUI _mainMenuUI;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _cardsButton;
        [SerializeField] private Button _themeButton;
        [SerializeField] private Transform _themesParent;
        [SerializeField] private Transform _cardsParent;
        [SerializeField] private GameObject _itemPrefab;

        private readonly List<EachThemeCardButton> _themes = new List<EachThemeCardButton>();
        private readonly List<EachThemeCardButton> _cardGroups = new List<EachThemeCardButton>();

        private void Start()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);
            _cardsButton.onClick.AddListener(OnCardButtonClicked);
            _themeButton.onClick.AddListener(OnThemeButtonClicked);

            PoolService.AddObjectToPool(PoolItemIds.THEME_CARD_ITEM, _itemPrefab);
        }

        private void OnEnable()
        {
            EventService.Subscribe<bool>(GameEvents.ON_RESOURCES_LOADED, OnResourceLoaded);
        }

        private void OnDisable()
        {
            EventService.Unsubscribe<bool>(GameEvents.ON_RESOURCES_LOADED, OnResourceLoaded);
        }

        public override void Show(bool isImmediate = false)
        {
            base.Show(isImmediate);
            OnCardButtonClicked();
        }

        private void OnResourceLoaded(bool result)
        {
            _themes.Clear();
            _cardGroups.Clear();

            _cardsParent.gameObject.SetActive(true);
            _themesParent.gameObject.SetActive(true);

            var themes = ThemeService.GetAllThemes();
            for (int i = 0; i < themes.Length; i++)
            {
                var item = PoolService.GetItem(PoolItemIds.THEME_CARD_ITEM);
                item.transform.SetParent(_themesParent);
                item.transform.localScale = Vector3.one;
                var eachTheme = item.GetComponent<EachThemeCardButton>();

                eachTheme.Initialize(themes[i].Name, themes[i].Thumb, OnThemeSelect);

                if (ThemeService.IsThemeSelected(themes[i].Name))
                {
                    eachTheme.Select();
                }
                else
                {
                    eachTheme.Deselect();
                }

                _themes.Add(eachTheme);
            }

            var cardGroups = ThemeService.GetAllCardGroups();
            for (int i = 0; i < cardGroups.Length; i++)
            {
                var item = PoolService.GetItem(PoolItemIds.THEME_CARD_ITEM);
                item.transform.SetParent(_cardsParent);
                item.transform.localScale = Vector3.one;
                var eachCardGroup = item.GetComponent<EachThemeCardButton>();

                eachCardGroup.Initialize(cardGroups[i].Name, cardGroups[i].Thumb, OnCardGroupSelect);

                if (ThemeService.IsCardGroupSelected(cardGroups[i].Name))
                {
                    eachCardGroup.Select();
                }
                else
                {
                    eachCardGroup.Deselect();
                }

                _cardGroups.Add(eachCardGroup);
            }
        }

        private void OnThemeSelect(string themeName)
        {
            ThemeService.SelectTheme(themeName);
            for (int i = 0; i < _themes.Count; i++)
            {
                if (_themes[i].Name.Equals(themeName))
                {
                    _themes[i].Select();
                }
                else
                {
                    _themes[i].Deselect();
                }
            }
        }

        private void OnCardGroupSelect(string cardGroupName)
        {
            ThemeService.SelectCardGroup(cardGroupName);
            for (int i = 0; i < _cardGroups.Count; i++)
            {
                if (_cardGroups[i].Name.Equals(cardGroupName))
                {
                    _cardGroups[i].Select();
                }
                else
                {
                    _cardGroups[i].Deselect();
                }
            }
        }

        private void OnBackButtonClicked()
        {
            _mainMenuUI.Show();
        }

        private void OnCardButtonClicked()
        {
            _cardsParent.gameObject.SetActive(true);
            _themesParent.gameObject.SetActive(false);
        }

        private void OnThemeButtonClicked()
        {
            _cardsParent.gameObject.SetActive(false);
            _themesParent.gameObject.SetActive(true);
        }
    }
}