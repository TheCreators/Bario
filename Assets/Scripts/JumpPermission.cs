using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class JumpPermission : MonoBehaviour
{
    [SerializeField] private float _allowedDistanceFromGroundToJump;
    
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public bool AllowedToJump => TryGetDistanceFromGround(_rigidbody, out float distanceFromGround)
                                 && distanceFromGround <= _allowedDistanceFromGroundToJump;
    
    private static bool TryGetDistanceFromGround(Rigidbody2D rigidBody, out float distance)
    {
        distance = 0;
        var raycastHit = Physics2D.Raycast(rigidBody.position, Vector2.down);
        
        if (raycastHit.collider == null) return false;
        
        distance = raycastHit.distance;
        return true;

    }
}
