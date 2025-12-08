using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _verticalAxis;
    [SerializeField] private Transform _horizontalAxis;

    [SerializeField] private PidRegulator _xAxisController = new PidRegulator(0, 0, 0);
    [SerializeField] private PidRegulator _yAxisController = new PidRegulator(0, 0, 0);
    
    [SerializeField] private Transform _hoverTransform;
    [SerializeField] private float _verticalAimingSpeed;
    
    private Vector3 _targetPoint;

    public void SetTarget(Vector3 input)
    {
        _targetPoint = input;
    }

    private void Update()
    {
        Vector3 hoverForward = _hoverTransform.forward;
        Vector3 up = _verticalAxis.up;
        Vector3 forward = _verticalAxis.forward;
        Vector3 aimDirection = (_targetPoint - _verticalAxis.position).normalized;
        
        float verticalAngle = Vector3.SignedAngle(_hoverTransform.forward, forward, up);
        float needAngle = verticalAngle + Vector3.SignedAngle(forward, aimDirection, up);
        
        _verticalAxis.rotation = Quaternion.AngleAxis(_yAxisController.Tick(verticalAngle, needAngle, Time.deltaTime) * Time.deltaTime * _verticalAimingSpeed, up) * _verticalAxis.rotation;
    }
}
