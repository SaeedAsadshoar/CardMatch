using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.UI.Panels.Elements
{
    public class EachDifficultyButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _difficultyLabel;
        private Action<int, int> _onSelect;
        private int _width;
        private int _height;

        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            _onSelect?.Invoke(_width, _height);
        }

        public void Initialize(int width, int height, Action<int, int> onSelect)
        {
            _width = width;
            _height = height;
            _onSelect = onSelect;
            _difficultyLabel.text = $"{width} X {height}";
        }
    }
}