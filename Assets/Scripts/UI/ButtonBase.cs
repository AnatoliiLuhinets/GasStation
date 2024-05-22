using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class ButtonBase : MonoBehaviour
    {
        public virtual event Action<ButtonBase> ButtonClicked;
        [SerializeField] protected Button Button;

        private void Awake()
        {
            Button.onClick.AddListener(OnButtonClicked);
        }

        protected virtual void OnButtonClicked()
        {
            ButtonClicked?.Invoke(this);
        }
    }
}