using Component.EventSystem;
using Domain.Constants;
using Domain.EventClasses;
using UnityEngine;
using UnityEngine.Audio;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;

        [SerializeField] private AudioSource _mainMenuAudio;
        [SerializeField] private AudioSource _gameLoopAudio;
        [SerializeField] private AudioSource _errorAudio;
        [SerializeField] private AudioSource _successAudio;
        [SerializeField] private AudioSource _winAudio;
        [SerializeField] private AudioSource _loseAudio;


        private void OnEnable()
        {
            EventService.Subscribe<bool>(GameEvents.ON_SPLASH_SCREEN_COMPLETED, OnSplashScreenCompleted);
            EventService.Subscribe<bool>(GameEvents.ON_BACK_TO_HOME_PRESSED, OnBackToHomePressed);
            EventService.Subscribe<OnGameFinished>(GameEvents.ON_GAME_FINISHED, OnGameFinished);
            EventService.Subscribe<bool>(GameEvents.ON_CARDS_CHECKED, OnCardsChecked);
            EventService.Subscribe<bool>(GameEvents.ON_GAME_START, OnGameStart);
            EventService.Subscribe<OnGameStartLoading>(GameEvents.ON_GAME_START_LOADING, OnGameStartLoading);
            EventService.Subscribe<float>(GameEvents.ON_MUSIC_VOLUME_CHANGED, OnMusicVolumeChanged);
            EventService.Subscribe<float>(GameEvents.ON_SFX_VOLUME_CHANGED, OnSfxVolumeChanged);
        }

        private void OnDisable()
        {
            EventService.Unsubscribe<bool>(GameEvents.ON_SPLASH_SCREEN_COMPLETED, OnSplashScreenCompleted);
            EventService.Unsubscribe<bool>(GameEvents.ON_BACK_TO_HOME_PRESSED, OnBackToHomePressed);
            EventService.Unsubscribe<OnGameFinished>(GameEvents.ON_GAME_FINISHED, OnGameFinished);
            EventService.Unsubscribe<bool>(GameEvents.ON_CARDS_CHECKED, OnCardsChecked);
            EventService.Unsubscribe<bool>(GameEvents.ON_GAME_START, OnGameStart);
            EventService.Unsubscribe<OnGameStartLoading>(GameEvents.ON_GAME_START_LOADING, OnGameStartLoading);
            EventService.Unsubscribe<float>(GameEvents.ON_MUSIC_VOLUME_CHANGED, OnMusicVolumeChanged);
            EventService.Unsubscribe<float>(GameEvents.ON_SFX_VOLUME_CHANGED, OnSfxVolumeChanged);
        }

        private void OnSplashScreenCompleted(bool obj)
        {
            _mainMenuAudio.Play();
        }

        private void OnBackToHomePressed(bool obj)
        {
            _mainMenuAudio.Play();
        }

        private void OnGameFinished(OnGameFinished obj)
        {
            _gameLoopAudio.Stop();
            if (obj.IsWin)
            {
                _winAudio.Play();
            }
            else
            {
                _loseAudio.Play();
            }
        }

        private void OnCardsChecked(bool obj)
        {
            if (obj)
            {
                _successAudio.Play();
            }
            else
            {
                _errorAudio.Play();
            }
        }

        private void OnGameStartLoading(OnGameStartLoading obj)
        {
            _mainMenuAudio.Stop();
        }

        private void OnGameStart(bool obj)
        {
            _gameLoopAudio.Play();
        }

        private void OnMusicVolumeChanged(float value)
        {
            _audioMixer.SetFloat(AudioMixerExposeParameters.MUSIC_VOLUME, value);
        }

        private void OnSfxVolumeChanged(float value)
        {
            _audioMixer.SetFloat(AudioMixerExposeParameters.SFX_VOLUME, value);
        }
    }
}