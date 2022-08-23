using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Vector2 _moveInput;
    private Collider2D _collider;
    private LayerMask _platformLayerMask;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _platformLayerMask = LayerMask.GetMask(Layers.Platform);
    }

    private void Update()
    {
        TryMove();
    }

    private void TryMove()
    {
        var velocity = new Vector2(_moveInput.x * moveSpeed, _rigidBody.velocity.y);
        _rigidBody.velocity = velocity;
    }

    private void OnMove(InputValue value)
    {
        var input = value.Get<Vector2>();
        _moveInput = input;
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed is false || _collider.IsTouchingLayers(_platformLayerMask) is false) return;
        
        var velocity = new Vector2(0, jumpForce);
        _rigidBody.velocity = velocity;
    }
}
