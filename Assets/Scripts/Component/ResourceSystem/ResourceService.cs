using System.Threading.Tasks;
using Component.AddressableSystem;
using Component.ThemeSystem;
using Component.UniqueIdSystem;
using Domain.Constants;
using Domain.Data;
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
            var themesTask = LoadAddressable<Themes>(0, 50, AddressableKeys.THEMES, 2);
            while (!themesTask.IsCompleted)
            {
                await Task.Yield();
            }

            await Task.Delay(2000);
            var cardGroupsTask = LoadAddressable<CardGroups>(50, 100, AddressableKeys.CARD_GROUPS, 2);
            while (!cardGroupsTask.IsCompleted)
            {
                await Task.Yield();
            }

            ThemeService.LoadThemes(themesTask.Result);
            ThemeService.LoadCardGroups(cardGroupsTask.Result);

            await Task.Delay(2000);

            _progress = 1;
        }

        private static async Task<T> LoadAddressable<T>(float startPercent, float maxPercent, string key, int maxCount)
        {
            _progress = startPercent;
            var id = UniqueIdService.GetUniqueId();
            var task = AddressableService.DownloadDependency<T>(id, key);
            while (!task.IsCompleted)
            {
                _progress = (startPercent + (float)(AddressableService.GetProgressPercentage(id) / (float)maxCount)) / 100.0f;
                await Task.Yield();
            }

            var taskDl = AddressableService.GetAddressable<T>(key);
            await taskDl;

            _progress = maxPercent / 100.0f;
            return taskDl.Result;
        }
    }
}