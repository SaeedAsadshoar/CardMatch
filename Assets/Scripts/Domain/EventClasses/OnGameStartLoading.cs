namespace Domain.EventClasses
{
    public class OnGameStartLoading
    {
        private readonly int _width;
        private readonly int _height;

        public int Width => _width;
        public int Height => _height;

        public OnGameStartLoading(int width, int height)
        {
            _width = width;
            _height = height;
        }
    }
}