using System;
using Component.EventSystem;
using Domain.Constants;
using Domain.EventClasses;
using Extension;
using Presentation.UI.Panels.Abstraction;
using TMPro;
using UnityEngine;

namespace Presentation.UI.Panels
{
    public class GameUI : UIPanel
    {
        [SerializeField] private TextMeshProUGUI _timeLabel;
        [SerializeField] private TextMeshProUGUI _scoreLabel;
        [SerializeField] private TextMeshProUGUI _comboLabel;
        [SerializeField] private GameObject _comboPlace;

        private void OnEnable()
        {
            EventService.Subscribe<int>(GameEvents.ON_GAME_REST_TIME_CHANGED, OnGameRestTimeChanged);
            EventService.Subscribe<int>(GameEvents.ON_GAME_TIME_CHANGED, OnGameTimeChanged);
            EventService.Subscribe<OnScoreChanged>(GameEvents.ON_SCORE_CHANGED, OnScoreChanged);
            EventService.Subscribe<OnGameStartLoading>(GameEvents.ON_GAME_START_LOADING, OnGameStartLoading);
        }

        private void OnDisable()
        {
            EventService.Unsubscribe<int>(GameEvents.ON_GAME_REST_TIME_CHANGED, OnGameRestTimeChanged);
            EventService.Unsubscribe<int>(GameEvents.ON_GAME_TIME_CHANGED, OnGameTimeChanged);
            EventService.Unsubscribe<OnScoreChanged>(GameEvents.ON_SCORE_CHANGED, OnScoreChanged);
            EventService.Unsubscribe<OnGameStartLoading>(GameEvents.ON_GAME_START_LOADING, OnGameStartLoading);
        }

        public override void Show(bool isImmediate = false)
        {
            base.Show(isImmediate);
            _timeLabel.text = StringExtension.SecondToTimeString_MS(0);
        }

        private void OnGameTimeChanged(int time)
        {
            _timeLabel.text = StringExtension.SecondToTimeString_MS(time);
        }

        private void OnGameRestTimeChanged(int time)
        {
            _timeLabel.text = StringExtension.SecondToTimeString_MS(time);
        }

        private void OnScoreChanged(OnScoreChanged onScoreChanged)
        {
            _scoreLabel.text = onScoreChanged.Score.ToString();

            if (onScoreChanged.Combo > 0)
            {
                _comboPlace.SetActive(true);
                _comboLabel.text = onScoreChanged.Combo.ToString();
            }
            else
            {
                _comboPlace.SetActive(false);
            }
        }

        private void OnGameStartLoading(OnGameStartLoading onGameStartLoading)
        {
            _comboLabel.text = "0";
            _comboPlace.SetActive(false);
            if (onGameStartLoading.IsFirstTime)
            {
                _scoreLabel.text = "0";
            }
        }
    }
}