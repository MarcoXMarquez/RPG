using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int damage = 10;
    public Transform attackPoint;
    public float weaponRange;
    public LayerMask playerLayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController2D player = collision.gameObject.GetComponent<PlayerController2D>();
            player.ChangeHealth(-damage);
        }
    }
    public void Attack()
    {
        Debug.Log("atacando");
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);
        if(hits.Length > 0)
        {
            hits[0].GetComponent<PlayerController2D>().ChangeHealth(-damage);   
        }
    }
}
