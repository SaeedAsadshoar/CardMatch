using Component.EventSystem;
using Component.StorageSystem;
using Domain.Constants;
using Presentation.UI.Panels.Abstraction;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.UI.Panels.MainMenu
{
    public class SettingPanel : UIPanel
    {
        [SerializeField] private MainMenuUI _mainMenuUI;
        [SerializeField] private Button _backButton;

        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;

        private void Start()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);

            _musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
            _sfxSlider.onValueChanged.AddListener(OnSfxSliderValueChanged);

            var musicVolume = StorageService.GetData(ConstDataNames.MUSIC_VOLUME, -25.0f);
            var sfxVolume = StorageService.GetData(ConstDataNames.SFX_VOLUME, 0.0f);
            _musicSlider.value = musicVolume;
            _sfxSlider.value = sfxVolume;
        }

        private void OnBackButtonClicked()
        {
            _mainMenuUI.Show();
        }

        private void OnMusicSliderValueChanged(float value)
        {
            EventService.Invoke(GameEvents.ON_MUSIC_VOLUME_CHANGED, value);
            StorageService.SetData(ConstDataNames.MUSIC_VOLUME, value);
        }

        private void OnSfxSliderValueChanged(float value)
        {
            EventService.Invoke(GameEvents.ON_SFX_VOLUME_CHANGED, value);
            StorageService.SetData(ConstDataNames.SFX_VOLUME, value);
        }
    }
}