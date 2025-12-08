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
            _controls.MoveInput = Vector3.forward * UnityEngine.Input.GetAxis("Vertical") + Vector3.right * UnityEngine.Input.GetAxis("Horizontal");
            _controls.LookPoint = _controls.AimingPoint = _freeLookCamera.Raycast();
        }
    }
}