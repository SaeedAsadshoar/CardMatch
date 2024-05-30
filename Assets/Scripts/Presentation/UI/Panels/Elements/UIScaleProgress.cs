using Presentation.UI.Panels.Abstraction;
using UnityEngine;

namespace Presentation.UI.Panels.Elements
{
    public class UIScaleProgress : UIProgress
    {
        private const float FILL_SPEED = 1;

        [SerializeField] private Transform _loadingIndicatorTransform;

        private float _curScale;
        private Vector3 _scale;
        private bool _checkProgress;

        public override bool IsComplete => 1 - _scale.x < 0.01f;

        public override void Reset()
        {
            _checkProgress = false;
            _scale = _loadingIndicatorTransform.localScale;
            _scale.x = 0;
            _curScale = 0;
            _loadingIndicatorTransform.localScale = _scale;
        }

        public override void SetProgress(float value)
        {
            _checkProgress = true;
            _curScale = value;
        }

        private void Update()
        {
            if (!_checkProgress) return;

            _scale = _loadingIndicatorTransform.localScale;
            if (_scale.x < _curScale)
            {
                _scale.x += FILL_SPEED * Time.deltaTime;
                _loadingIndicatorTransform.localScale = _scale;
            }
        }
    }
}