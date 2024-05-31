using System.Collections;
using Component.EventSystem;
using Domain.Constants;
using UnityEngine;

namespace Presentation.Game.Card
{
    public class CardAnimation : MonoBehaviour
    {
        [SerializeField] private BoxCollider _touchCollider;

        [Header("Animations"), SerializeField] private Animation _cardAnimation;
        [SerializeField] private AnimationClip _showCardAnimation;
        [SerializeField] private AnimationClip _hideCardAnimation;
        [SerializeField] private AnimationClip _successCardAnimation;
        [SerializeField] private AudioSource _flipAudio;

        private WaitForSeconds _waitForShowAnimationTime;
        private WaitForSeconds _waitForHideAnimationTime;

        private bool _isFlipping;
        public bool IsFlipping => _isFlipping;

        private void Awake()
        {
            _waitForShowAnimationTime = new WaitForSeconds(_showCardAnimation.length);
            _waitForHideAnimationTime = new WaitForSeconds(_hideCardAnimation.length);
        }

        private void OnEnable()
        {
            _touchCollider.enabled = false;
        }

        public void ShowCard()
        {
            StopCoroutine(nameof(_HideCard));
            StartCoroutine(nameof(_ShowCard));
        }

        public void HideCard()
        {
            StopCoroutine(nameof(_ShowCard));
            StartCoroutine(nameof(_HideCard));
        }

        private IEnumerator _ShowCard()
        {
            _touchCollider.enabled = false;
            _isFlipping = true;
            _flipAudio.Play();
            _cardAnimation.Play(_showCardAnimation.name);
            yield return _waitForShowAnimationTime;
            _isFlipping = false;
        }

        private IEnumerator _HideCard()
        {
            _touchCollider.enabled = false;
            _isFlipping = true;
            _flipAudio.Play();
            _cardAnimation.Play(_hideCardAnimation.name);
            yield return _waitForHideAnimationTime;
            _isFlipping = false;
            _touchCollider.enabled = true;
        }

        public void Success()
        {
            _touchCollider.enabled = false;
            _cardAnimation.Play(_successCardAnimation.name);
            _isFlipping = true;
        }
    }
}