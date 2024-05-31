using Component.EventSystem;
using Domain.Constants;
using Presentation.UI.Panels.Abstraction;
using UnityEngine;

namespace Presentation.UI.Panels
{
    public class LoadingUI : UIPanel
    {
        [SerializeField] private UIProgress _uiProgress;

        private void OnEnable()
        {
            EventService.Subscribe<float>(GameEvents.ON_GAME_LOAD_PROGRESS_CHANGED, OnGameLoadProgressChanged);
            EventService.Subscribe<bool>(GameEvents.ON_GAME_FINISH_LOADING, OnGameFinishLoading);
        }

        private void OnDisable()
        {
            EventService.Unsubscribe<float>(GameEvents.ON_GAME_LOAD_PROGRESS_CHANGED, OnGameLoadProgressChanged);
            EventService.Unsubscribe<bool>(GameEvents.ON_GAME_FINISH_LOADING, OnGameFinishLoading);
        }

        public void Reset()
        {
            _uiProgress.Reset();
        }

        private void OnGameFinishLoading(bool result)
        {
            _uiProgress.SetProgress(1);
        }

        private void OnGameLoadProgressChanged(float progress)
        {
            _uiProgress.SetProgress(progress);
        }
    }
}