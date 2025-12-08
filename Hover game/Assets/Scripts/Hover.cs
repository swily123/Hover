using Input;
using UnityEngine;

[RequireComponent(typeof(ConstantForce))]
public class Hover : MonoBehaviour
{
    [SerializeField] private float _moveForce;
    [SerializeField] private float _rotationTorque;
    [SerializeField] private PidRegulator _regulator = new PidRegulator(0, 0, 0);
    [SerializeField] private Gun _gun;
    
    private IHoverControls _hoverControls;
    private HoverControlsInfo _controlInfo;
    private ConstantForce _constantForce;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _hoverControls = GetComponent<IHoverControls>();
        _controlInfo = _hoverControls.GetControls();
        _constantForce = GetComponent<ConstantForce>();
    }

    private void Update()
    {
        Vector3 forward = _transform.forward;
        Vector3 up = _transform.up;
        float currentAngle = Vector3.SignedAngle(forward, _controlInfo.LookPoint - _transform.position, up);
        
        float angle = _regulator.Tick(currentAngle, 0, Time.deltaTime);
        _constantForce.relativeTorque = Vector3.up * (angle * _rotationTorque);
        _constantForce.relativeForce = _controlInfo.MoveInput * _moveForce;
        
        _gun.SetTarget(_controlInfo.AimingPoint);
    }
}