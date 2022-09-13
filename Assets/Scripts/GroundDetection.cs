using UnityEngine;

[RequireComponent(typeof(Transform))]
public class GroundDetection : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private bool _drawGizmos;

    private const float AllowedDistanceFromGroundToJump = 0.1f;

    public bool IsGrounded
    {
        get
        {
            var bounds = _collider.bounds;
            var raycastHit = Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, AllowedDistanceFromGroundToJump);
            return raycastHit.collider != null;
        }
    }
    
    private void OnDrawGizmos()
    {
        if (_drawGizmos is false) return;
        
        var bounds = _collider.bounds;
        
        // Draw ground detection box
        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawWireCube(new Vector3(bounds.center.x, bounds.center.y - AllowedDistanceFromGroundToJump, bounds.center.z), bounds.size);
        
        // Draw collider bounds box
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(new Vector3(bounds.center.x, bounds.center.y, bounds.center.z), bounds.size);
    }
}
