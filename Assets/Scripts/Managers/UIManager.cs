using Presentation.UI.Panels;
using Presentation.UI.Panels.Abstraction;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private SplashScreenUI _splashScreenPanel;
        [SerializeField] private UIPanel _mainMenuPanel;
        [SerializeField] private LoadingUI _loadingPanel;
        public bool IsSplashComplete => _splashScreenPanel.IsSplashComplete;

        public static UIManager Instance;
        private void Awake()
        {
            Instance = this;
            
            _splashScreenPanel.gameObject.SetActive(true);
            _mainMenuPanel.gameObject.SetActive(true);
            _loadingPanel.gameObject.SetActive(true);

            _splashScreenPanel.Show(true);
            _mainMenuPanel.Hide(true);
            _loadingPanel.Hide(true);
        }

        public void SetLoadProgress(float progress)
        {
            if (_splashScreenPanel.IsShow)
            {
                _splashScreenPanel.SetProgress(progress);
            }
            else
            {
                _loadingPanel.SetProgress(progress);
            }
        }

        public void InitLoadProgress()
        {
            if (_splashScreenPanel.IsShow)
            {
                _splashScreenPanel.Reset();
            }
            else
            {
                _loadingPanel.Reset();
            }
        }

        public void ShowMainMenu()
        {
            _splashScreenPanel.Hide(false);
            _mainMenuPanel.Show(false);
        }
    }
}