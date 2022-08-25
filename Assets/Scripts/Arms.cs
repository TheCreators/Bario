using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms : MonoBehaviour
{
    public bool IsTouchingSomething { get; set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsTouchingSomething = true;
    }

    private void OnCollisionExit2D(Collision2D otherCollider)
    {
        IsTouchingSomething = false;
    }
}
