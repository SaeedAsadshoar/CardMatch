using System.Collections;
using Presentation.UI.Panels.Interface;
using UnityEngine;

namespace Presentation.UI.Panels.Abstraction
{
    public class UIPanel : MonoBehaviour, IUIPanel
    {
        private const float PANEL_SHOW_SPEED = 2f;
        private const float PANEL_HIDE_SPEED = 3f;

        private CanvasGroup _canvasGroup;
        private bool _isShow;

        public bool IsShow => _isShow;

        private void Awake()
        {
            Initialize();
        }

        public virtual void Initialize()
        {
            if (_canvasGroup == null)
            {
                _canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }

        public virtual void Show(bool isImmediate = false)
        {
            StopCoroutine(nameof(HidePanel));

            if (isImmediate)
            {
                ShowHideImmediate(true);
            }
            else
            {
                StartCoroutine(nameof(ShowPanel));
            }
        }

        public virtual void Hide(bool isImmediate = false)
        {
            StopCoroutine(nameof(ShowPanel));

            if (isImmediate)
            {
                ShowHideImmediate(false);
            }
            else
            {
                StartCoroutine(nameof(HidePanel));
            }
        }

        private void ShowHideImmediate(bool isShow)
        {
            _isShow = isShow;
            _canvasGroup.alpha = isShow ? 1 : 0;
            _canvasGroup.blocksRaycasts = isShow;
            _canvasGroup.interactable = isShow;
        }

        private IEnumerator ShowPanel()
        {
            _isShow = true;
            while (_canvasGroup.alpha < 1)
            {
                _canvasGroup.alpha += PANEL_SHOW_SPEED * Time.deltaTime;
                yield return null;
            }

            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }

        private IEnumerator HidePanel()
        {
            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= PANEL_HIDE_SPEED * Time.deltaTime;
                yield return null;
            }

            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
            _isShow = false;
        }
    }
}