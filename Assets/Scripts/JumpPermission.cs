using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Transform))]
public class JumpPermission : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;

    private const float AllowedDistanceFromGroundToJump = 0.1f;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public bool AllowedToJump => TryGetDistanceFromGround(out float distanceFromGround)
                                 && distanceFromGround <= AllowedDistanceFromGroundToJump;
    
    private bool TryGetDistanceFromGround(out float distance)
    {
        distance = 0;
        
        var position = _rigidbody.position;
        var vector = new Vector2(position.x, position.y - transform.localScale.y * _collider.size.y / 2);
        var raycastHit = Physics2D.Raycast(vector, Vector2.down);

        if (raycastHit.collider == null) return false;

        distance = raycastHit.distance;
        return true;
    }
}
