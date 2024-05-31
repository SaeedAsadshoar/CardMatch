using Component.EventSystem;
using Domain.Constants;
using Domain.EventClasses;
using Presentation.UI.Panels.Abstraction;
using Presentation.UI.Panels.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.UI.Panels
{
    public class GameDifficultyUI : UIPanel
    {
        [SerializeField] private MainMenuUI _mainMenuUI;
        [SerializeField] private Button _backButton;
        [SerializeField] private GameObject _eachDifficultyButtonPrefab;
        [SerializeField] private Transform _parent;

        private bool _isInitialized;

        private void Start()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);
        }

        private void OnBackButtonClicked()
        {
            _mainMenuUI.Show();
        }

        public override void Show(bool isImmediate = false)
        {
            if (!_isInitialized)
            {
                for (int i = 2; i < 7; i++)
                {
                    for (int j = 2; j < 7; j++)
                    {
                        if ((i * j) % 2 != 0) continue;
                        GameObject ins = Instantiate(_eachDifficultyButtonPrefab, _parent);
                        ins.transform.localScale = Vector3.one;
                        ins.GetComponent<EachDifficultyButton>().Initialize(i, j, OnSelectDifficulty);
                    }
                }

                _isInitialized = true;
            }

            base.Show(isImmediate);
        }

        private void OnSelectDifficulty(int width, int height)
        {
            EventService.Invoke<OnGameStartLoading>(GameEvents.ON_GAME_START_LOADING, new OnGameStartLoading(width, height, true));
        }
    }
}