using System;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.UI.Panels.Elements
{
    public class EachThemeCardButton : MonoBehaviour
    {
        [SerializeField] private RawImage _thumb;
        [SerializeField] private GameObject _isSelectedObj;
        [SerializeField] private Button _button;

        private string _name;
        private Action<string> _onClick;

        public string Name => _name;

        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        public void Initialize(string itemName, Texture2D thumb, Action<string> onClick)
        {
            _onClick = onClick;
            _name = itemName;
            _thumb.texture = thumb;
        }

        private void OnButtonClicked()
        {
            _onClick?.Invoke(_name);
        }

        public void Select()
        {
            _isSelectedObj.SetActive(true);
        }

        public void Deselect()
        {
            _isSelectedObj.SetActive(false);
        }
    }
}