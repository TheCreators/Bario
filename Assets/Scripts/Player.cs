using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SurfaceDetector))]
public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Vector2 _moveInput;
    private bool _jumpPressed;
    private RaycastHit2D _raycastHit;
    private SurfaceDetector _surfaceDetector;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _surfaceDetector = GetComponent<SurfaceDetector>();
    }

    private void FixedUpdate()
    {
        TryMove();
        TryJump();
    }

    private void TryMove()
    {
        var velocity = new Vector2(_moveInput.x * _moveSpeed, _rigidBody.velocity.y);
        _rigidBody.velocity = velocity;
    }

    private void TryJump()
    {
        if (_jumpPressed is false || _surfaceDetector.IsGrounded is false) return;

        var velocity = new Vector2(_rigidBody.velocity.x, _jumpForce);
        _rigidBody.velocity = velocity;
    }

    private void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        _jumpPressed = value.isPressed;
    }
}