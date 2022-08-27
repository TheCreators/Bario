using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Transform))]
public class ObstaclesDetector : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;

    private const float DistanceFromObstacleToTurnAround = 0.1f;
    private Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public bool NecessityToTurnAround => TryGetDistanceFromNearestObstacle(out float distanceFromGround)
                                         && distanceFromGround <= DistanceFromObstacleToTurnAround;

    // Update is called once per frame
    private bool TryGetDistanceFromNearestObstacle(out float distance)
    {
        distance = 0;

        var position = _rigidbody.position;
        var rightVector = new Vector2(position.x + transform.localScale.x * _collider.size.y / 2, position.y);
        var leftVector = new Vector2(position.x - transform.localScale.x * _collider.size.y / 2, position.y);
        var leftRaycastHit = Physics2D.Raycast(leftVector, Vector2.left);
        var rightRaycastHit = Physics2D.Raycast(rightVector, Vector2.right);

        if (leftRaycastHit.collider == null && rightRaycastHit) return false;

        distance = leftRaycastHit.distance > rightRaycastHit.distance ? rightRaycastHit.distance : leftRaycastHit.distance;
        return true;
    }
}