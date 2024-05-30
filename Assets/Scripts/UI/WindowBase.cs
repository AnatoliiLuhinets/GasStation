using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class WindowBase : MonoBehaviour
    {
        public event Action<WindowBase> OnWindowOpen;

        [field: SerializeField] protected GameObject MainPanel { get; private set; }
        [field: SerializeField] protected Button CloseButton { get; private set; }
        
        private bool _hasOpen;

        private void Awake() => OnAwake();

        private void OnDestroy() => Destroyed();

        protected virtual void OnAwake()
        {
            CloseButton.onClick.AddListener(Close);
        }

        public virtual void OpenPanel()
        {
            if (_hasOpen)
            {
                return;
            }

            _hasOpen = true;

            UpdateVisibleState(_hasOpen);
            
            OnWindowOpen?.Invoke(this);
        }

        public virtual void Close()
        {
            _hasOpen = false;

            UpdateVisibleState(_hasOpen);
        }

        private void UpdateVisibleState(bool state)
        {
            gameObject.SetActive(state);
        }
        
        protected virtual void Destroyed()
        {
            CloseButton.onClick.RemoveListener(Close);
        }
    }
}
