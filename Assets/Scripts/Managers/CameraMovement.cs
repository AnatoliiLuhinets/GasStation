using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float _panSpeed = 0.5f;
        
        private Vector3 _lastMousePosition;
        private Vector2 _initialAngle;
        private float _currentYRotation;
        private float _maxRotationAngle = 30f;

        private void Start()
        {
            _initialAngle = transform.eulerAngles;
            _currentYRotation = 0f;
        }

        private void Update()
        {
            if (IsPointerOverUIElement())
                return;

            if (Input.GetMouseButton(0))
            {
                Vector3 deltaPosition = Input.mousePosition - _lastMousePosition;
                float yRotationDelta = deltaPosition.x * _panSpeed * Time.deltaTime;
                _currentYRotation += yRotationDelta;

                _currentYRotation = Mathf.Clamp(_currentYRotation, -_maxRotationAngle, _maxRotationAngle);

                transform.rotation = Quaternion.Euler(_initialAngle.x, _initialAngle.y + _currentYRotation, 0);
            }
            _lastMousePosition = Input.mousePosition;
        }

        private bool IsPointerOverUIElement()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return true;
            
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    return true;
            }

            return false;
        }
    }
}