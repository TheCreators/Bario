using UnityEngine;

public class SurfaceDetector : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;

    [Header("Tuning")]
    [Space]
    [SerializeField] [Range(0, 1)] private float _groundDetectionBoxOffset = 0.1f;
    [SerializeField] [Range(0, 1)] private float _wallDetectionBoxOffset = 0.1f;

    [Header("Gizmos")]
    [Space]
    [SerializeField] private bool _drawColliderBox;
    [SerializeField] private bool _drawGroundDetectionBox;
    [SerializeField] private bool _drawWallDetectionBox;
    [Space]
    [SerializeField] private Color _colliderColor = Color.white;
    [SerializeField] private Color _touchingColor = Color.green;
    [SerializeField] private Color _notTouchingColor = Color.red;

    public bool IsGrounded => IsTouchingSurface(Vector2.down, _groundDetectionBoxOffset);

    public bool IsTouchingWall =>
        IsTouchingSurface(Vector2.right, _wallDetectionBoxOffset) || IsTouchingSurface(Vector2.left, _wallDetectionBoxOffset);

    private bool IsTouchingSurface(Vector2 direction, float offset)
    {
        var bounds = _collider.bounds;
        var raycastHit = Physics2D.BoxCast(bounds.center, bounds.size, 0f, direction, offset);
        return raycastHit.collider != null;
    }

    private void OnDrawGizmos()
    {
        var bounds = _collider.bounds;

        if (_drawGroundDetectionBox)
        {
            Gizmos.color = IsGrounded ? _touchingColor : _notTouchingColor;
            Gizmos.DrawWireCube(new Vector3(bounds.center.x, bounds.center.y - _groundDetectionBoxOffset, bounds.center.z), bounds.size);
        }

        if (_drawWallDetectionBox)
        {
            Gizmos.color = IsTouchingWall ? _touchingColor : _notTouchingColor;
            Gizmos.DrawWireCube(bounds.center, new Vector3(bounds.size.x + _wallDetectionBoxOffset * 2, bounds.size.y, bounds.size.z));
        }

        if (_drawColliderBox)
        {
            Gizmos.color = _colliderColor;
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }
    }
}
