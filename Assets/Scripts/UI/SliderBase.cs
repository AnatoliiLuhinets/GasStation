using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class SliderBase : MonoBehaviour
    {
        public event Action<float> SliderValueChanged;
        [SerializeField] protected Slider Slider;

        private void Awake()
        {
            Slider.onValueChanged.AddListener(OnSliderChanged);
        }

        protected virtual void OnSliderChanged(float value)
        {
            SliderValueChanged?.Invoke(value);
        }
    }
}