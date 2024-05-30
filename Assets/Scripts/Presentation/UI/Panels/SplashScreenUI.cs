using System.Threading.Tasks;
using Component.EventSystem;
using Domain.Constants;
using Presentation.UI.Panels.Abstraction;
using TMPro;
using UnityEngine;

namespace Presentation.UI.Panels
{
    public class SplashScreenUI : UIPanel
    {
        [SerializeField] private UIProgress _uiProgress;
        [SerializeField] private TextMeshProUGUI _versionLabel;

        private void OnEnable()
        {
            _versionLabel.text = $"V {Application.version}";
            EventService.Subscribe<float>(GameEvents.ON_SPLASH_SCREEN_LOAD_PROGRESS_CHANGED, OnSplashScreenLoadProgressChanged);
            EventService.Subscribe<bool>(GameEvents.ON_RESOURCES_LOADED, OnResourceLoaded);
        }

        private void OnDisable()
        {
            _versionLabel.text = $"V {Application.version}";
            EventService.Unsubscribe<float>(GameEvents.ON_SPLASH_SCREEN_LOAD_PROGRESS_CHANGED, OnSplashScreenLoadProgressChanged);
            EventService.Unsubscribe<bool>(GameEvents.ON_RESOURCES_LOADED, OnResourceLoaded);
        }

        private async void OnResourceLoaded(bool result)
        {
            while (!_uiProgress.IsComplete)
            {
                await Task.Delay(10);
            }
            
            await Task.Delay(1000);
            
            EventService.Invoke<bool>(GameEvents.ON_SPLASH_SCREEN_COMPLETED,true);
        }

        private void OnSplashScreenLoadProgressChanged(float progress)
        {
            _uiProgress.SetProgress(progress);
        }

        public void Reset()
        {
            _uiProgress.Reset();
        }
    }
}