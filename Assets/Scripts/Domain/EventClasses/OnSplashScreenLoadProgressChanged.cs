namespace Domain.EventClasses
{
    public class OnSplashScreenLoadProgressChanged
    {
        private float _progress;

        public float Progress => _progress;

        public OnSplashScreenLoadProgressChanged(float progress)
        {
            _progress = progress;
        }

        public void SetProgress(float progress)
        {
            _progress = progress;
        }
    }
}