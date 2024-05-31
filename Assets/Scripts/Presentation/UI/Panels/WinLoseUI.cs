using Component.EventSystem;
using Domain.Constants;
using Domain.EventClasses;
using Extension;
using Presentation.UI.Panels.Abstraction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.UI.Panels
{
    public class WinLoseUI : UIPanel
    {
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private GameObject _losePanel;

        [SerializeField] private GameObject[] _newRecordObjs;

        [SerializeField] private TextMeshProUGUI _scoreLabel;
        [SerializeField] private TextMeshProUGUI _extraTimeLabel;
        [SerializeField] private TextMeshProUGUI[] _winCountLabel;

        [SerializeField] private Button[] _homeButtons;
        [SerializeField] private Button _nextLevel;

        private void Start()
        {
            foreach (var homeButton in _homeButtons)
            {
                homeButton.onClick.AddListener(OnHomeButtonClicked);
            }

            _nextLevel.onClick.AddListener(OnNextLevelButtonClicked);
        }

        private void OnEnable()
        {
            EventService.Subscribe<OnGameFinished>(GameEvents.ON_GAME_FINISHED, OnGameFinished);
        }

        private void OnDisable()
        {
            EventService.Subscribe<OnGameFinished>(GameEvents.ON_GAME_FINISHED, OnGameFinished);
        }

        private void OnGameFinished(OnGameFinished onGameFinished)
        {
            if (onGameFinished.IsWin)
            {
                _winPanel.SetActive(true);
                _losePanel.SetActive(false);

                _extraTimeLabel.text = StringExtension.SecondToTimeString_MS(onGameFinished.ExtraTime);
            }
            else
            {
                _winPanel.SetActive(false);
                _losePanel.SetActive(true);
            }

            foreach (var ugui in _winCountLabel)
            {
                ugui.text = onGameFinished.WinCount.ToString();
            }

            foreach (var newRecordObj in _newRecordObjs)
            {
                newRecordObj.SetActive(onGameFinished.IsNewRecord);
            }

            _scoreLabel.text = onGameFinished.Score.ToString();
        }

        private void OnHomeButtonClicked()
        {
            EventService.Invoke(GameEvents.ON_BACK_TO_HOME_PRESSED, true);
        }

        private void OnNextLevelButtonClicked()
        {
            EventService.Invoke(GameEvents.ON_GAME_START_LOADING, new OnGameStartLoading());
        }
    }
}