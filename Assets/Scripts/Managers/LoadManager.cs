using System.Threading.Tasks;
using Component.EventSystem;
using Component.ResourceSystem;
using Domain.Constants;
using UnityEngine;

namespace Managers
{
    public class LoadManager : MonoBehaviour
    {
        private void Start()
        {
            Invoke(nameof(StartLoadGame), 1);
        }

        private async void StartLoadGame()
        {
            Task loadTask = ResourceService.InitializeGame();
            float preProgress = ResourceService.Progress;

            while (!loadTask.IsCompleted)
            {
                if (ResourceService.Progress - preProgress >= 0.01f)
                {
                    preProgress = ResourceService.Progress;
                    EventService.Invoke<float>(GameEvents.ON_SPLASH_SCREEN_LOAD_PROGRESS_CHANGED, ResourceService.Progress);
                }

                await Task.Yield();
            }

            EventService.Invoke<bool>(GameEvents.ON_RESOURCES_LOADED, true);
        }
    }
}