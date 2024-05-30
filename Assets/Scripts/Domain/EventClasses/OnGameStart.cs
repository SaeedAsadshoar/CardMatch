namespace Domain.EventClasses
{
    public class OnGameStart
    {
        private readonly int _width;
        private readonly int _height;

        public int Width => _width;
        public int Height => _height;

        public OnGameStart(int width, int height)
        {
            _width = width;
            _height = height;
        }
    }
}