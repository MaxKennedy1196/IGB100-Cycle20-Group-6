using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//Abstract enemy parent class to be inherited from for child enemy types 
public class Enemy : MonoBehaviour
{
    public int health = 5; //Set to 5 for testing, needs to be reset once individual enemies are created
    public int damage;
    public int moveSpeed;
    public int attackRange;

    public GameObject deathEffect;

    public PlayerMovement player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Reference to player
        player = GameManager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        //Moving towards the player, made method so can be changed in children enemies if necessary (dash and constant movement types?)
        Movement();

        //Attempting to attack the player if in range of them
        //if (Vector2.Distance(player.transform.position, transform.position) <= attackRange) { Attack(); }
    }

    //Default movement, enemy consistently moves towards player
    public void Movement()
    {
        float step = moveSpeed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
    }

    //public abstract void Attack();

    public void Die()
    {
        //Create a death effect at the location of the enemy when they die
        //Instantiate(deathEffect, transform.position);
        Destroy(this.gameObject);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) { Die(); }
    }
}
