using UnityEngine;

public class Coin : MonoBehaviour
{
    private static Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject != _player.gameObject) return;

        Destroy(gameObject);
    }
}