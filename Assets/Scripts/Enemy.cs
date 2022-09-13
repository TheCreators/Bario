using System.Collections;
using Enums;
using UnityEngine;

[RequireComponent(typeof(SurfaceDetector))]
public class Enemy : MonoBehaviour
{
    private SurfaceDetector _surfaceDetector;
    private Rigidbody2D _rigidBody;

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
        _surfaceDetector = GetComponent<SurfaceDetector>();
        _jumpCurrentVerticalForce = _jumpBaseVerticalForce;
        _jumpCurrentHorizontalForce = _jumpBaseHorizontalForce;
    }

    private void FixedUpdate()
    {
        TryJump();
    }

    private void TryJump()
    {
        if (_surfaceDetector.IsGrounded is false) return;

        TryChangeDirection();

        var velocity = new Vector2(_jumpCurrentHorizontalForce * (int)_jumpDirection, _jumpCurrentVerticalForce);
        _rigidBody.velocity = velocity;

        ChangeSpeed();
    }

    private void TryChangeDirection()
    {
        if (_surfaceDetector.IsTouchingWall is false) return;

        _jumpDirection = _jumpDirection == HorizontalDirection.Left ? HorizontalDirection.Right : HorizontalDirection.Left;
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
}