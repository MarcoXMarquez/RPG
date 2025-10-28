using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 2f;
    private int facingDirection = 1;

    private Transform player;
    private bool isChasing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isChasing || player == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // Calculate direction
        Vector2 direction = (player.position - transform.position).normalized;

        // Flip only if player is on the opposite side
        if (player.position.x > transform.position.x && facingDirection < 0)
        {
            Flip();
        }
        else if (player.position.x < transform.position.x && facingDirection > 0)
        {
            Flip();
        }

        // Move toward the player
        rb.linearVelocity = direction * speed;
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(facingDirection, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;
            isChasing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rb.linearVelocity = Vector2.zero;
            isChasing = false;
        }
    }
}
