using Presentation.UI.Panels.Abstraction;
using TMPro;
using UnityEngine;

namespace Presentation.UI.Panels
{
    public class SplashScreenUI : UIPanel
    {
        [SerializeField] private UIProgress _uiProgress;
        [SerializeField] private TextMeshProUGUI _versionLabel;
        public bool IsSplashComplete => _uiProgress.IsComplete;

        private void OnEnable()
        {
            _versionLabel.text = $"V {Application.version}";
        }

        public void SetProgress(float progress)
        {
            _uiProgress.SetProgress(progress);
        }

        public void Reset()
        {
            _uiProgress.Reset();
        }
    }
}