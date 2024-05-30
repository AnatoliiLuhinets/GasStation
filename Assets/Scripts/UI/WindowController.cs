using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class WindowController : MonoBehaviour
    {
        [SerializeField] private List<WindowBase> _windows;

        private WindowBase CurrentWindow;

        private void Awake()
        {
            foreach (var item in _windows)
            {
                item.OnWindowOpen += ChangeWindow;
            }
        }

        private void ChangeWindow(WindowBase window)
        {
            if (CurrentWindow && CurrentWindow != window)
            {
                CurrentWindow.Close();
            }

            CurrentWindow = window;
        }

        private void OnDestroy()
        {
            foreach (var item in _windows)
            {
                item.OnWindowOpen -= ChangeWindow;
            }
        }
    }
}
