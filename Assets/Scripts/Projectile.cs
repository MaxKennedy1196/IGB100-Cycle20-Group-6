using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;

    //Internal boolean to check if the projectile has dealt damage to an enemy
    private bool damageDealt = false;

    public float projectileSpeed;
    public float projectileLifetime;
    public Transform target;

    public void Start()
    {
        Destroy(this.gameObject, projectileLifetime);
        //transform.LookAt(target);
        //transform.right = target.position - transform.position;

        Vector2 direction = target.position - transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);

    }

    public void Update()
    {
        transform.position += transform.up * Time.deltaTime * projectileSpeed;
        //Vector2MoveTowards 
    }

    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.transform.tag == "Enemy")
    //    {   
    //        if (!damageDealt)
    //        {
    //            Enemy enemy = collision.transform.GetComponent<Enemy>();
    //            enemy.TakeDamage(damage);
    //            enemy.Die();
    //            damageDealt = true;
    //        }
    //    }
    //}
}
