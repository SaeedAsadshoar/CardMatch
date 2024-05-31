using Domain.Data;

namespace Component.SettingSystem
{
    public static class SettingService
    {
        private static Settings _settings;

        public static float EachCardRestTime => _settings.EachCardRestTime;
        public static float EachCardShowTime => _settings.EachCardShowTime;
        public static float CardWidth => _settings.CardWidth;
        public static float CardHeight => _settings.CardHeight;
        public static float PanelShowSpeed => _settings.PanelShowSpeed;
        public static float PanelHideSpeed => _settings.PanelHideSpeed;
        public static float LoadingsFillSpeed => _settings.LoadingsFillSpeed;

        public static void LoadSetting(Settings settings)
        {
            _settings = settings;
        }
    }
}