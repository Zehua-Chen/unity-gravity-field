using UnityEngine;

namespace GravityField.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        RectTransform _progressElement = null;
        RectTransform _rectTransform = null;

        float _progress = 0.0f;

        public float Width
        {
            get
            {
                float left = _rectTransform.rect.xMin;
                float right = _rectTransform.rect.xMax;

                return right - left;
            }
        }

        public float Progress
        {
            get
            {
                return _progress;
            }
            set
            {
                if (value > 1.0f)
                {
                    return;
                }

                _progressElement.SetSizeWithCurrentAnchors(
                    RectTransform.Axis.Horizontal,
                    value * this.Width);
                _progress = value;
            }
        }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }
    }
}