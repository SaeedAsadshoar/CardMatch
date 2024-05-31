namespace Domain.EventClasses
{
    public class OnGameStartLoading
    {
        private readonly int _width;
        private readonly int _height;
        private readonly bool _isFirstTime;

        public int Width => _width;
        public int Height => _height;
        public bool IsFirstTime => _isFirstTime;

        public OnGameStartLoading(int width, int height, bool isFirstTime)
        {
            _width = width;
            _height = height;
            _isFirstTime = isFirstTime;
        }

        public OnGameStartLoading(bool isFirstTime = false)
        {
            _width = -1;
            _height = -1;
            _isFirstTime = isFirstTime;
        }
    }
}