namespace Presentation.UI.Panels.Interface
{
    public interface IUIProgress
    {
        bool IsComplete { get; }
        void Reset();
        void SetProgress(float value);
    }
}