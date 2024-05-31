using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class ToggleBase : MonoBehaviour
    {
        public event Action<bool> ToggleClicked;
        [SerializeField] protected Toggle Toggle;
        
        private void Awake()
        {
            OnAwake();
        }
        
        protected virtual void OnAwake()
        {
            Toggle.onValueChanged.AddListener(OnToggleClicked);
        }

        protected virtual void OnToggleClicked(bool isActive)
        {
            ToggleClicked?.Invoke(isActive);
        }

        public void ChangeActiveState(bool state)
        {
            Toggle.isOn = state;
        }
        
        protected virtual void OnDestroy()
        {
            Toggle.onValueChanged.RemoveListener(OnToggleClicked);
        }
    }
}
