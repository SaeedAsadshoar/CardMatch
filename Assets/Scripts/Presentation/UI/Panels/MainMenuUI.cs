using Presentation.UI.Panels.Abstraction;
using Presentation.UI.Panels.MainMenu;
using UnityEngine;

namespace Presentation.UI.Panels
{
    public class MainMenuUI : UIPanel
    {
        [SerializeField] private MainPanel _mainPanel;
        [SerializeField] private SettingPanel _settingPanel;
        [SerializeField] private ThemePanel _themePanel;
        [SerializeField] private GameDifficultyUI _gameDifficultyUI;

        private void Start()
        {
            _mainPanel.gameObject.SetActive(true);
            _settingPanel.gameObject.SetActive(true);
            _themePanel.gameObject.SetActive(true);
            _gameDifficultyUI.gameObject.SetActive(true);

            _mainPanel.InitializeEvents(OnStartGameButtonClicked, OnSettingButtonClicked, OnThemeButtonClicked);
        }

        public override void Show(bool isImmediate)
        {
            base.Show(isImmediate);
            _mainPanel.Show(true);
            _settingPanel.Hide(true);
            _themePanel.Hide(true);
            _gameDifficultyUI.Hide(true);
        }

        private void OnStartGameButtonClicked()
        {
            _mainPanel.Hide(false);
            _settingPanel.Hide(false);
            _themePanel.Hide(false);
            _gameDifficultyUI.Show(false);
        }

        private void OnSettingButtonClicked()
        {
            _mainPanel.Hide(false);
            _settingPanel.Show(false);
            _themePanel.Hide(false);
            _gameDifficultyUI.Hide(false);
        }

        private void OnThemeButtonClicked()
        {
            _mainPanel.Hide(false);
            _settingPanel.Hide(false);
            _themePanel.Show(false);
            _gameDifficultyUI.Hide(false);
        }
    }
}