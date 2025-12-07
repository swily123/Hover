using System;
using UnityEngine;

namespace Input
{
    public class PlayerInput : MonoBehaviour, IHoverControls
    {
        [Range(1, 5)][SerializeField] private float _sensitivity;
        [SerializeField] private FreeLookCamera _freeLookCamera;
        
        private HoverControlsInfo _controls = new HoverControlsInfo();
        
        public HoverControlsInfo GetControls()
        {
            return _controls;
        }

        private void Update()
        {
            _freeLookCamera.Rotate(UnityEngine.Input.GetAxis("Mouse X") * _sensitivity, UnityEngine.Input.GetAxis("Mouse Y") * _sensitivity);

            _controls.LookPoint = _controls.AimingPoint = _freeLookCamera.Raycast();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(_controls.LookPoint, 1f);
        }
    }
}