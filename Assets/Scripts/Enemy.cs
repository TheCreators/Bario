using System.Collections;
using Enums;
using UnityEngine;

[RequireComponent(typeof(JumpPermission))]
public class Enemy : MonoBehaviour
{
    private JumpPermission _jumpPermission;
    private Rigidbody2D _rigidBody;
    private Arms _arms;

    [Header("Jumping")] 
    [SerializeField] private HorizontalDirection _jumpDirection;
    [SerializeField] private float _jumpBaseHorizontalForce;
    [SerializeField] private float _jumpBaseVerticalForce;
    [SerializeField] private float _jumpMinimalVerticalForce;
    [SerializeField] private float _jumpVerticalForceReducing;
    [SerializeField] private float _jumpHorizontalForceReducing;
    [SerializeField] private float _delayBetweenJumpsSeries;

    private float _jumpCurrentVerticalForce;
    private float _jumpCurrentHorizontalForce;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _jumpPermission = GetComponent<JumpPermission>();
        _arms = GetComponentInChildren<Arms>();
        _jumpCurrentVerticalForce = _jumpBaseVerticalForce;
        _jumpCurrentHorizontalForce = _jumpBaseHorizontalForce;
    }
    
    private void Update()
    {
        TryJump();
        TryChangeDirection();
    }

    private void TryJump()
    {
        if (_jumpPermission.AllowedToJump is false) return;
        
        var velocity = new Vector2(_jumpCurrentHorizontalForce * (int) _jumpDirection, _jumpCurrentVerticalForce);
        _rigidBody.velocity = velocity;
        
        ChangeSpeed();
    }
    
    private bool JumpsSeriesFinished => _jumpCurrentVerticalForce < _jumpMinimalVerticalForce;

    private void ChangeSpeed()
    {
        if (JumpsSeriesFinished)
        {
            StartCoroutine(PauseMovement(_delayBetweenJumpsSeries));
            return;
        }
        
        _jumpCurrentVerticalForce -= _jumpVerticalForceReducing;
        _jumpCurrentHorizontalForce -= _jumpHorizontalForceReducing;
    }
    
    private IEnumerator PauseMovement(float seconds)
    {
        _jumpCurrentHorizontalForce = 0;
        _jumpCurrentVerticalForce = 0;
        yield return new WaitForSeconds(seconds);
        _jumpCurrentHorizontalForce = _jumpBaseHorizontalForce;
        _jumpCurrentVerticalForce = _jumpBaseVerticalForce;
    }

    private void TryChangeDirection()
    {
        if (_arms.IsTouchingSomething is false) return;

        _jumpDirection = _jumpDirection == HorizontalDirection.Left ? HorizontalDirection.Right : HorizontalDirection.Left;

        _arms.IsTouchingSomething = false;
    }
}