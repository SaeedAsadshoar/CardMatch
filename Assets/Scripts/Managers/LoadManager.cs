using System.Threading.Tasks;
using Component.ResourceSystem;
using UnityEngine;

namespace Managers
{
    public class LoadManager : MonoBehaviour
    {
        private void Start()
        {
            UIManager.Instance.InitLoadProgress();
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
                    UIManager.Instance.SetLoadProgress(preProgress);
                }

                await Task.Yield();
            }

            while (!UIManager.Instance.IsSplashComplete)
            {
                await Task.Yield();
            }

            UIManager.Instance.ShowMainMenu();
        }
    }
}