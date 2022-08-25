using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Legs _legs;
    private Arms _arms;

    [SerializeField] private int moveDirection;
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float baseJumpForce;
    [SerializeField] private float _movementSlowing;
    private float _currentMoveSpeed;
    private float _currentJumpForce;
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _legs = GetComponentInChildren<Legs>();
        _arms = GetComponentInChildren<Arms>();
        _currentJumpForce = baseJumpForce;
        _currentMoveSpeed = baseMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CollisionsChecking();
    }

    void Move()
    {
        if (!_legs.IsTouchingGround)
        {
            return;
        }
        
        var velocity = new Vector2(_currentMoveSpeed * moveDirection, _currentJumpForce);
        _rigidBody.velocity = velocity;
    }

    void CollisionsChecking()
    {
        if (_legs.IsTouchingGround)
        {
            _currentJumpForce -= _movementSlowing;
            _currentMoveSpeed -= _movementSlowing;
            if (_currentJumpForce < 1)
            {
                StartCoroutine(DisableMove());
            }
        }
        
        if (_arms.IsTouchingSomething)
        {
            moveDirection = -moveDirection;
            _arms.IsTouchingSomething = false;
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