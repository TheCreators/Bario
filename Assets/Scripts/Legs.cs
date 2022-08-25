using UnityEngine;

public class Legs : MonoBehaviour
{
    public bool IsTouchingGround { get; private set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsTouchingGround = true;
    }

    private void OnCollisionExit2D(Collision2D otherCollider)
    {
        IsTouchingGround = false;
    }
}
