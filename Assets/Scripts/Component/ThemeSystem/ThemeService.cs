using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Component.AddressableSystem;
using Component.StorageSystem;
using Domain.Constants;
using Domain.Data;
using UnityEngine;

namespace Component.ThemeSystem
{
    public static class ThemeService
    {
        private static Themes _themes;
        private static CardGroups _cardGroups;

        private static int _currentThemeIndex = 0;
        private static int _currentCardsIndex = 0;

        public static void LoadThemes(Themes themes)
        {
            _themes = themes;
            _currentThemeIndex = StorageService.GetData(ConstDataNames.CURRENT_THEME_INDEX, 0);
            _currentCardsIndex = StorageService.GetData(ConstDataNames.CURRENT_CARDS_INDEX, 0);
        }

        public static void LoadCardGroups(CardGroups cardGroups)
        {
            _cardGroups = cardGroups;
        }

        public static Texture2D GetThemeSprite()
        {
            return _themes.AllThemes[_currentThemeIndex].Thumb;
        }

        public static async Task<Cards> GetCards()
        {
            var task = AddressableService.GetAddressable<Cards>(_cardGroups.AllCardGroups[_currentCardsIndex].Key);
            await task;
            return task.Result;
        }
    }
}