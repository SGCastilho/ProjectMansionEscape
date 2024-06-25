using System.Collections;
using UnityEngine;

namespace Core.GameCamera
{
    public sealed class GameplayCamera : MonoBehaviour
    {
        private const float CAMERA_Z_PADDING = -10f;

        [Header("Settings")]
        [SerializeField] private Transform _trackingObject;
        [SerializeField] private bool _stopTracking;
        [Space(12)]
        [SerializeField] private float _cameraYPadding;
        [Space(20f)]
        [SerializeField] private float _cameraAlertIn = 16f;
        [SerializeField] private float _cameraAlertOut = 1.2f;

        private Transform _transform;
        private Camera _camera;

        private bool _cameraAlert;
        private bool _cameraAlertEnd;
        private float _orthograficLerping;

        private void Awake()
        {
            _transform = transform;
            _camera = GetComponent<Camera>();
        }

        private void Start()
        {
            if(_trackingObject == null)
            {
                enabled = false;
                return;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                CallCameraAlert();
            }

            CameraAlert();
        }

        private void CameraAlert()
        {
            if (_cameraAlert && !_cameraAlertEnd)
            {
                _orthograficLerping = Mathf.Lerp(_camera.orthographicSize, 3f, _cameraAlertIn * Time.deltaTime);
                _camera.orthographicSize = _orthograficLerping;

                if (_orthograficLerping < 3.1f)
                {
                    _camera.orthographicSize = 3f;
                    _cameraAlertEnd = true;
                }
            }

            if (_cameraAlertEnd)
            {
                _orthograficLerping = Mathf.Lerp(_camera.orthographicSize, 5f, _cameraAlertOut * Time.deltaTime);
                _camera.orthographicSize = _orthograficLerping;

                if (_orthograficLerping >= 4.99f)
                {
                    _camera.orthographicSize = 5f;
                    _cameraAlert = false;
                    _cameraAlertEnd = false;
                }
            }
        }

        private void LateUpdate()
        {
            if(!_stopTracking)
            {
                _transform.position = new Vector3(_trackingObject.position.x, _cameraYPadding, CAMERA_Z_PADDING);
            }
        }

        public void CallCameraAlert()
        {
            if(_cameraAlert == true) return;

            _cameraAlert = true;
        }
    }
}
