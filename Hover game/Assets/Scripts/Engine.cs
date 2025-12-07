using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _maxDistance = 2f;
    [SerializeField] private float _maxForce;
    [SerializeField] private float _damping;
    [SerializeField] private float _progressivity = 1f;
    [SerializeField] private float _upFactor = 0.75f;
    
    private Transform _transform;
    private Vector3 _engineWorldSpeed;
    private Vector3 _engineWorldOldPosition;
    
    private void Awake()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        var forward = _transform.forward;
        Vector3 forceDirection = Vector3.up;
            
        if (Physics.Raycast(_transform.position, forward, out RaycastHit hit, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
        {
            Lift(forward, hit.distance, out forceDirection);
        }

        Damping(forward);
    }

    private void Lift(Vector3 forward, float distance, out Vector3 forceDirection)
    {
        float forceFactor = Mathf.Pow(Mathf.Clamp01(1f - distance / _maxDistance), _progressivity);
        forceDirection = Vector3.Slerp(-forward, Vector3.up, _upFactor);
        
        _rigidbody.AddForceAtPosition(forceDirection * (_maxForce * forceFactor), _transform.position, ForceMode.Force);
    }

    private void Damping(Vector3 forceDirection)
    {
        _engineWorldSpeed = (_transform.position - _engineWorldOldPosition) * Time.fixedDeltaTime;
        float dotResult = Mathf.Clamp01(Vector3.Dot(forceDirection.normalized, _engineWorldSpeed.normalized));
        _rigidbody.AddForceAtPosition(-forceDirection * (_engineWorldSpeed.magnitude * dotResult * _damping), _transform.position, ForceMode.Force);
        
        _engineWorldOldPosition = _transform.position;
    }
}