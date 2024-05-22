using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] protected Button CloseButton;
        [SerializeField] protected GameObject MainPanel;
    
        private void Awake() => OnAwake();
        protected virtual void OnAwake() => CloseButton?.onClick.AddListener(OnCloseButtonClicked);
        private void OnDestroy() => Destroyed();
        protected virtual void Destroyed() => CloseButton?.onClick.RemoveAllListeners();
        protected virtual void OnCloseButtonClicked() => MainPanel.SetActive(false);
        public abstract void OpenPanel();
    }
}
