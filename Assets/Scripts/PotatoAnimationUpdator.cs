using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class PotatoAnimationUpdator : MonoBehaviour
{
    // Vitesse minimale pour déclencher l'animation de déplacement
    public float moveTriggerVelocityThreshold = 0.5f;
    
    private NavMeshAgent _agent;
    private Animator _animator;
    private Vector2 _smoothDeltaPosition = Vector2.zero;
    private Vector2 _velocity = Vector2.zero;
    private static readonly int Move = Animator.StringToHash("move");
    private static readonly int VelX = Animator.StringToHash("velx");
    private static readonly int VelY = Animator.StringToHash("vely");
    
    private Vector3 _lastPosition = Vector3.zero;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var t = transform;
        var position = t.position;
        var worldDeltaPosition = _lastPosition - position;
        
        // Map 'worldDeltaPosition' to local space
        var dx = Vector3.Dot(t.right, worldDeltaPosition);
        var dy = Vector3.Dot(t.forward, worldDeltaPosition);
        var deltaPosition = new Vector2(dx, dy);

        // Low-pass filter the deltaMove
        var smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        _smoothDeltaPosition = Vector2.Lerp(_smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if time advances
        if (Time.deltaTime > 1e-5f)
            _velocity = _smoothDeltaPosition / Time.deltaTime;

        var shouldMove = _velocity.magnitude > moveTriggerVelocityThreshold;
        
        _animator.SetBool(Move, shouldMove);
        _animator.SetFloat(VelX, _velocity.x);
        _animator.SetFloat(VelY, _velocity.y);

        _lastPosition = position;
    }
}