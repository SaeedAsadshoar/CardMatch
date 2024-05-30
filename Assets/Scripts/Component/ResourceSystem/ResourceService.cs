using System.Threading.Tasks;
using UnityEngine;

namespace Component.ResourceSystem
{
    public static class ResourceService
    {
        private static float _progress;
        public static float Progress => _progress;

        public static async Task InitializeGame()
        {
            _progress = 0;
            while (_progress < 1)
            {
                _progress += 0.05f;
                await Task.Delay(100);
            }

            _progress = 1;
        }
    }
}