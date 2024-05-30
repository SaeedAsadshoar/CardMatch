using Presentation.UI.Panels.Interface;
using UnityEngine;

namespace Presentation.UI.Panels.Abstraction
{
    public abstract class UIProgress : MonoBehaviour, IUIProgress
    {
        public virtual bool IsComplete { get; }

        public virtual void Reset()
        {
            throw new System.NotImplementedException();
        }

        public virtual void SetProgress(float value)
        {
            throw new System.NotImplementedException();
        }

    }
}