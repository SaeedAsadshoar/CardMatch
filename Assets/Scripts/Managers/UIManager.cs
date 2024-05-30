using Component.EventSystem;
using Domain.Constants;
using Domain.EventClasses;
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

        private void Awake()
        {
            _splashScreenPanel.gameObject.SetActive(true);
            _mainMenuPanel.gameObject.SetActive(true);
            _loadingPanel.gameObject.SetActive(true);

            _splashScreenPanel.Reset();

            _splashScreenPanel.Show(true);
            _mainMenuPanel.Hide(true);
            _loadingPanel.Hide(true);
        }

        private void OnEnable()
        {
            EventService.Subscribe<OnGameStart>(GameEvents.ON_GAME_START, OnGameStart);
            EventService.Subscribe<bool>(GameEvents.ON_SPLASH_SCREEN_COMPLETED, OnSplashScreenCompleted);
        }

        private void OnDisable()
        {
            EventService.Unsubscribe<OnGameStart>(GameEvents.ON_GAME_START, OnGameStart);
            EventService.Unsubscribe<bool>(GameEvents.ON_SPLASH_SCREEN_COMPLETED, OnSplashScreenCompleted);
        }

        private void OnSplashScreenCompleted(bool result)
        {
            ShowMainMenu();
        }

        private void OnGameStart(OnGameStart onGameStart)
        {
            _loadingPanel.Reset();
            _splashScreenPanel.Hide();
            _mainMenuPanel.Hide();
            _loadingPanel.Show();
        }

        private void ShowMainMenu()
        {
            _splashScreenPanel.Hide();
            _mainMenuPanel.Show();
        }
    }
}