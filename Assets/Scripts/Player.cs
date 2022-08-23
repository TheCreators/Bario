using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Vector2 moveInput;
    private BoxCollider2D collider2D;
    private LayerMask platformLayerMask;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<BoxCollider2D>();
        platformLayerMask = LayerMask.GetMask(Layers.Platform);
    }

    private void Update()
    {
        TryMove();
    }

    private void TryMove()
    {
        var velocity = new Vector2(moveInput.x * moveSpeed, rigidBody.velocity.y);
        rigidBody.velocity = velocity;
    }

    private void OnMove(InputValue value)
    {
        var input = value.Get<Vector2>();
        moveInput = input;
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed is false || collider2D.IsTouchingLayers(platformLayerMask) is false) return;
        
        var velocity = new Vector2(0, jumpForce);
        rigidBody.velocity = velocity;
    }
}
