using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private BoxCollider2D _collider2D;
    private LayerMask _platformLayerMask;


    [SerializeField] private int moveDirection = 1;
    [SerializeField] private float baseMoveSpeed = 6;
    [SerializeField] private float baseJumpForce = 6;
    private float _currentMoveSpeed;
    private float _currentJumpForce;

    private float _movementSlowing = 1;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<BoxCollider2D>();
        _platformLayerMask = LayerMask.GetMask("Platform"); //надо будет заменить на Енум
        _currentJumpForce = baseJumpForce;
        _currentMoveSpeed = baseMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (!_collider2D.IsTouchingLayers(_platformLayerMask))
        {
            return;
        }

        Vector2 velocity;
        velocity = new Vector2(_currentMoveSpeed * moveDirection, _currentJumpForce);
        _rigidBody.velocity = velocity;

        if (_collider2D.IsTouchingLayers(_platformLayerMask))
        {
            _currentJumpForce -= _movementSlowing;
            _currentMoveSpeed -= _movementSlowing;
            if (_currentJumpForce < 1)
            {
                StartCoroutine(DisableMove());
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            moveDirection = -moveDirection;
        }
    }

    IEnumerator DisableMove()
    {
        _currentMoveSpeed = 0;
        _currentJumpForce = 0;
        yield return new WaitForSeconds(1);
        _currentMoveSpeed = baseMoveSpeed;
        _currentJumpForce = baseJumpForce;
    }
}