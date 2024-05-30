using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Component.AddressableSystem
{
    public static class AddressableService
    {
        private static readonly Dictionary<double, float> ProgressPercentage = new Dictionary<double, float>();

        /// <summary>
        /// download any dependency
        /// </summary>
        /// <param name="id">it use for get progress - try to define a unique id</param>
        /// <param name="key"></param>
        public static async Task DownloadDependency<T>(double id, string key)
        {
            ProgressPercentage.TryAdd(id, 0);
            var dependencies = Addressables.DownloadDependenciesAsync(key);
            await Task.Yield();
            while (!dependencies.IsDone)
            {
                SetProgress(id, dependencies.GetDownloadStatus());
                await Task.Yield();
            }
        }

        public static async Task UnLoadSceneAsync(double id, SceneInstance sceneInstance)
        {
            ProgressPercentage.TryAdd(id, 0);
            var unloadTask = Addressables.UnloadSceneAsync(sceneInstance);
            await Task.Yield();
            while (!unloadTask.IsDone)
            {
                SetProgress(id, unloadTask.GetDownloadStatus());
                await Task.Yield();
            }
        }
        
        public static async Task<T> GetAddressable<T>(string key)
        {
            var dependencies = Addressables.LoadAssetAsync<T>(key);
            await Task.Yield();
            while (!dependencies.IsDone)
            {
                await Task.Yield();
            }

            return dependencies.Result;
        }

        /// <summary>
        /// Get Current progress of download from 0 - 100 int number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static float GetProgressPercentage(double id)
        {
            if (ProgressPercentage.TryGetValue(id, out var percentage)) return percentage;
            return 0;
        }
        
        private static void SetProgress(double id, DownloadStatus downloadStatus)
        {
            if (downloadStatus.Percent == 0)
            {
                if (ProgressPercentage[id] < 95) ProgressPercentage[id] += Time.deltaTime;
            }
            else
            {
                var percent = downloadStatus.Percent * 100.0f;
                if (ProgressPercentage[id] < percent)
                {
                    ProgressPercentage[id] = percent;
                }
            }
        }
    }
}