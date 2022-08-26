using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Mathf;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Vector2 _moveInput;
    private RaycastHit2D _raycastHit;

    private float _distanceFromGround;
    private float _allowedDistanceToJump = 1.3f;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _raycastHit = Physics2D.Raycast(_rigidBody.position, Vector2.down);
        if (_raycastHit.collider != null)
        {
            _distanceFromGround = _raycastHit.distance;
        }
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
        if (_distanceFromGround > _allowedDistanceToJump) return;

        var velocity = new Vector2(0, _jumpForce);
        _rigidBody.velocity = velocity;
    }
}