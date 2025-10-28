using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController2D player = collision.gameObject.GetComponent<PlayerController2D>();
            player.ChangeHealth(-damage);
        }
    }
}
