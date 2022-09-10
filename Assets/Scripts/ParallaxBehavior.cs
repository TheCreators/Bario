using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class ParallaxBehavior : MonoBehaviour
{
    [SerializeField] private Transform _followingTarget;
    [SerializeField, Range(0f, 1f)] private float _parallaxStrength = 0.1f;
    [SerializeField] private bool _disableVerticalParallax;
    private Vector3 _targetPreviousPosition;

    private void Start()
    {
        if (_followingTarget == null)
        {
            Debug.Assert(UnityEngine.Camera.main != null, "UnityEngine.Camera.main != null");
            _followingTarget = UnityEngine.Camera.main.transform;
        }

        _targetPreviousPosition = _followingTarget.position;
    }

    private void FixedUpdate()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        var delta = _followingTarget.position - _targetPreviousPosition;
        
        if (_disableVerticalParallax is true) delta.y = 0;
        
        _targetPreviousPosition = _followingTarget.position;
        transform.position += delta * _parallaxStrength;
    }
}
