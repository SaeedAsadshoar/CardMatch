using System;
using Presentation.UI.Panels.Abstraction;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.UI.Panels.MainMenu
{
    public class MainPanel : UIPanel
    {
        [SerializeField] private MainMenuUI _mainMenuUI;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _themeButton;

        private Action _onStartGameClicked;
        private Action _onSettingClicked;
        private Action _onThemeClicked;

        private void Start()
        {
            _startGameButton.onClick.AddListener(OnStartGameButtonClicked);
            _settingButton.onClick.AddListener(OnSettingButtonClicked);
            _themeButton.onClick.AddListener(OnThemeButtonClicked);
        }

        public void InitializeEvents(Action onStartGameClicked, Action onSettingClicked, Action onThemeClicked)
        {
            _onStartGameClicked = onStartGameClicked;
            _onSettingClicked = onSettingClicked;
            _onThemeClicked = onThemeClicked;
        }

        private void OnStartGameButtonClicked()
        {
            _onStartGameClicked?.Invoke();
        }

        private void OnSettingButtonClicked()
        {
            _onSettingClicked?.Invoke();
        }

        private void OnThemeButtonClicked()
        {
            _onThemeClicked?.Invoke();
        }
    }
}