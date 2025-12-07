using System;
using UnityEngine;

namespace Input
{
    public class FreeLookCamera : MonoBehaviour
    {
        [Header("Camera")]
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _minAngle;
        [SerializeField] private float _maxAngle;
        
        [Header("Raycast")]
        [SerializeField] private float _maxlength = 300f;
        [SerializeField] private LayerMask _layerMask;
        
        private Transform _transform;
        private float _verticalAngle;
        private float _horizontalAngle ;
        
        private void Awake()
        {
            _transform = transform;
        }

        private void LateUpdate()
        {
            _transform.position = _target.position;
            _transform.rotation = Quaternion.Euler(_verticalAngle, _horizontalAngle, 0);
        }

        public void Rotate(float inputX, float inputY)
        {
            _horizontalAngle += inputX;
            
            _verticalAngle -= inputY;
            _verticalAngle = Mathf.Clamp(_verticalAngle, _minAngle, _maxAngle);
        }

        public Vector3 Raycast()
        {
            var forward = _cameraTransform.forward;
            
            if (Physics.Raycast(_cameraTransform.position, forward, out RaycastHit hit, _maxlength, _layerMask, QueryTriggerInteraction.Ignore))
            {
                return hit.point;
            }

            return _cameraTransform.forward + forward * _maxlength;
        }
    }
}