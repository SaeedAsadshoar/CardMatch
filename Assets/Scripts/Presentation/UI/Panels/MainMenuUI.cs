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

        public override void Show(bool isImmediate = false)
        {
            base.Show(isImmediate);
            _mainPanel.Show(true);
            _settingPanel.Hide(true);
            _themePanel.Hide(true);
            _gameDifficultyUI.Hide(true);
        }

        private void OnStartGameButtonClicked()
        {
            _mainPanel.Hide();
            _settingPanel.Hide();
            _themePanel.Hide();
            _gameDifficultyUI.Show();
        }

        private void OnSettingButtonClicked()
        {
            _mainPanel.Hide();
            _settingPanel.Show();
            _themePanel.Hide();
            _gameDifficultyUI.Hide();
        }

        private void OnThemeButtonClicked()
        {
            _mainPanel.Hide();
            _settingPanel.Hide();
            _themePanel.Show();
            _gameDifficultyUI.Hide();
        }
    }
}