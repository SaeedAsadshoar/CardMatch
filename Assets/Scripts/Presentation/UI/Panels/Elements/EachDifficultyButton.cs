using System;
using TMPro;
using UnityEngine;

namespace Presentation.UI.Panels.Elements
{
    public class EachDifficultyButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _difficultyLabel;

        public void Initialize(int width, int height, Action onSelect)
        {
            _difficultyLabel.text = $"{width} X {height}";
        }
    }
}