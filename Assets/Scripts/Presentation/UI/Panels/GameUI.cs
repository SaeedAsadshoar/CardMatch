using System;
using Component.EventSystem;
using Domain.Constants;
using Extension;
using Presentation.UI.Panels.Abstraction;
using TMPro;
using UnityEngine;

namespace Presentation.UI.Panels
{
    public class GameUI : UIPanel
    {
        [SerializeField] private TextMeshProUGUI _timeLabel;

        private void OnEnable()
        {
            EventService.Subscribe<int>(GameEvents.ON_GAME_REST_TIME_CHANGED, OnGameRestTimeChanged);
            EventService.Subscribe<int>(GameEvents.ON_GAME_TIME_CHANGED, OnGameTimeChanged);
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
    }
}