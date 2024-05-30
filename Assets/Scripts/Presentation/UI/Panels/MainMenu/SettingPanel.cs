using Presentation.UI.Panels.Abstraction;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.UI.Panels.MainMenu
{
    public class SettingPanel : UIPanel
    {
        [SerializeField] private MainMenuUI _mainMenuUI;
        [SerializeField] private Button _backButton;
        
        private void Start()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);
        }

        private void OnBackButtonClicked()
        {
            _mainMenuUI.Show(false);
        }
    }
}