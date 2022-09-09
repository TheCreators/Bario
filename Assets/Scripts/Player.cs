using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(JumpPermission))]
public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Vector2 _moveInput;
    private RaycastHit2D _raycastHit;
    private JumpPermission _jumpPermission;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _jumpPermission = GetComponent<JumpPermission>();
    }

    private void FixedUpdate()
    {
        TryMove();
    }

    private void TryMove()
    {
        var velocity = new Vector2(_moveInput.x * _moveSpeed, _rigidBody.velocity.y);
        _rigidBody.velocity = velocity;
    }

    private void OnMove(InputValue value)
    {
        var input = value.Get<Vector2>();
        _moveInput = input;
    }

    private void OnJump(InputValue value)
    {
        if (_jumpPermission.AllowedToJump is false) return;

        var velocity = new Vector2(0, _jumpForce);
        _rigidBody.velocity = velocity;
    }
}