using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Abstract enemy parent class to be inherited from for child enemy types 
public class Enemy : MonoBehaviour
{
    public GameManager Manager;
    public Player player;

    public int health = 5; //Set to 5 for testing, needs to be reset once individual enemies are created
    public int damage;
    public int moveSpeed;
    public int attackRange;

    public GameObject deathEffect;

    

    void Awake()
    {
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//find gamemanager
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();//find Player      

        Manager.enemyList.Add(gameObject);
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

    /*void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player" && Time.time > damageTime)
        {
            other.GetComponent<Player>().takeDamage(damage);
            damageTime = Time.time + damageRate;

        }
    }*/
}
