using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float _panSpeed = 0.5f;

        private Vector3 _lastInputPosition;
        private Vector2 _initialAngle;
        private float _currentYRotation;
        private float _maxRotationAngle = 15f;

        private void Start()
        {
            _initialAngle = transform.eulerAngles;
            _currentYRotation = 0f;
        }

        private void Update()
        {
            if (IsPointerOverUIElement())
                return;

            Vector3 inputPosition = GetInputPosition();

#if UNITY_EDITOR
            if (Input.GetMouseButton(0))
#else
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
#endif
            {
                Vector3 deltaPosition = inputPosition - _lastInputPosition;
                float yRotationDelta = deltaPosition.x * _panSpeed * Time.deltaTime;
                _currentYRotation += yRotationDelta;

                _currentYRotation = Mathf.Clamp(_currentYRotation, -_maxRotationAngle, _maxRotationAngle);

                transform.rotation = Quaternion.Euler(_initialAngle.x, _initialAngle.y + _currentYRotation, 0);
            }

            _lastInputPosition = inputPosition;
        }

        private Vector3 GetInputPosition()
        {
#if UNITY_EDITOR
            return Input.mousePosition;
#else
            if (Input.touchCount > 0)
            {
                return Input.GetTouch(0).position;
            }
            return new Vector3();
#endif
        }

        private bool IsPointerOverUIElement()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return true;

#if !UNITY_EDITOR
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(i).fingerId))
                    return true;
            }
#endif
            return false;
        }
    }
}
