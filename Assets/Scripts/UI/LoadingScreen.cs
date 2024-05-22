using System;
using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _rotationSpeed;
        
        private void Update()
        {
            _image.rectTransform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
        }
    }
}
