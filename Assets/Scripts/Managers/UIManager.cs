using System.Threading.Tasks;
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
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private WinLoseUI _winLoseUI;

        private void Awake()
        {
            _splashScreenPanel.gameObject.SetActive(true);
            _mainMenuPanel.gameObject.SetActive(true);
            _loadingPanel.gameObject.SetActive(true);
            _gameUI.gameObject.SetActive(true);
            _winLoseUI.gameObject.SetActive(true);

            _splashScreenPanel.Reset();

            _splashScreenPanel.Show(true);
            _mainMenuPanel.Hide(true);
            _loadingPanel.Hide(true);
            _gameUI.Hide(true);
            _winLoseUI.Hide(true);
        }

        private void OnEnable()
        {
            EventService.Subscribe<OnGameStartLoading>(GameEvents.ON_GAME_START_LOADING, OnGameStartLoading);
            EventService.Subscribe<bool>(GameEvents.ON_SPLASH_SCREEN_COMPLETED, OnSplashScreenCompleted);
            EventService.Subscribe<bool>(GameEvents.ON_GAME_FINISH_LOADING, OnGameFinishLoading);
            EventService.Subscribe<OnGameFinished>(GameEvents.ON_GAME_FINISHED, OnGameFinished);
            EventService.Subscribe<bool>(GameEvents.ON_BACK_TO_HOME_PRESSED, OnBackToHomePressed);
        }

        private void OnDisable()
        {
            EventService.Unsubscribe<OnGameStartLoading>(GameEvents.ON_GAME_START_LOADING, OnGameStartLoading);
            EventService.Unsubscribe<bool>(GameEvents.ON_SPLASH_SCREEN_COMPLETED, OnSplashScreenCompleted);
            EventService.Unsubscribe<bool>(GameEvents.ON_GAME_FINISH_LOADING, OnGameFinishLoading);
            EventService.Unsubscribe<OnGameFinished>(GameEvents.ON_GAME_FINISHED, OnGameFinished);
            EventService.Unsubscribe<bool>(GameEvents.ON_BACK_TO_HOME_PRESSED, OnBackToHomePressed);
        }

        private void OnSplashScreenCompleted(bool result)
        {
            ShowMainMenu();
        }

        private void OnGameStartLoading(OnGameStartLoading onGameStartLoading)
        {
            _loadingPanel.Reset();
            _splashScreenPanel.Hide();
            _mainMenuPanel.Hide();
            _winLoseUI.Hide();
            _loadingPanel.Show();
        }

        private void OnGameFinishLoading(bool result)
        {
            _loadingPanel.Hide();
            _gameUI.Show();
        }

        private async void OnGameFinished(OnGameFinished onGameFinished)
        {
            await Task.Delay(1000);
            _winLoseUI.Show();
            _gameUI.Hide();
        }

        private void ShowMainMenu()
        {
            _splashScreenPanel.Hide();
            _mainMenuPanel.Show();
        }

        private void OnBackToHomePressed(bool result)
        {
            _winLoseUI.Hide();
            _mainMenuPanel.Show();
        }
    }
}