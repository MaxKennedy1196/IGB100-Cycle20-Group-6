using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;

    //Internal boolean to check if the projectile has dealt damage to an enemy
    private bool damageDealt = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {   
            if (!damageDealt)
            {
                Enemy enemy = collision.transform.GetComponent<Enemy>();
                enemy.TakeDamage(damage);
                enemy.Die();
                damageDealt = true;
            }
        }
    }
}
