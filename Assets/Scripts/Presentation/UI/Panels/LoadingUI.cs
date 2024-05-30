using Presentation.UI.Panels.Abstraction;
using UnityEngine;

namespace Presentation.UI.Panels
{
    public class LoadingUI : UIPanel
    {
        [SerializeField] private UIProgress _uiProgress;

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