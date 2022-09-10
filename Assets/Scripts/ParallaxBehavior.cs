using UnityEngine;

public class ParallaxBehavior : MonoBehaviour
{
    [SerializeField] private Transform followingTarget;
    [SerializeField, Range(0f, 1f)] private float parallaxStrength = 0.1f;
    [SerializeField] private bool disableVerticalParallax;
    private Vector3 _targetPreviousPosition;
    void Start()
    {
        if (!followingTarget)
            followingTarget = UnityEngine.Camera.main.transform;
        _targetPreviousPosition = followingTarget.position;
    }

    void FixedUpdate()
    {
        var delta = followingTarget.position - _targetPreviousPosition;
        if (disableVerticalParallax is true) delta.y = 0;
        _targetPreviousPosition = followingTarget.position;
        transform.position += delta * parallaxStrength;
    }
}
