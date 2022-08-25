using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Vector2 _moveInput;
    private Legs _legs;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _legs = GetComponentInChildren<Legs>();
    }

    private void Update()
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
        if (value.isPressed is false || _legs.IsTouchingGround is false) return;

        var velocity = new Vector2(0, _jumpForce);
        _rigidBody.velocity = velocity;
    }
}