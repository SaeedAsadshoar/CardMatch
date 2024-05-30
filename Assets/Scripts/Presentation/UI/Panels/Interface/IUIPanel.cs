namespace Presentation.UI.Panels.Interface
{
    public interface IUIPanel
    {
        bool IsShow { get; }
        void Initialize();
        void Show(bool isImmediate);
        void Hide(bool isImmediate);
    }
}