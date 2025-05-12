using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Abstract enemy parent class to be inherited from for child enemy types 
public class Enemy : MonoBehaviour
{
    public GameManager Manager;
    public Player player;

    public EnemyStats stats;

    public float health;
    public float damage;
    float moveSpeed;
    public float attackRange;
    public float hungerProvided;
    //public float damageRate = 1.0f;
    //float damageTime;
    public SpriteRenderer spriteRenderer;

    public GameObject deathEffect;
    public AudioSource deathEffectSound;
    public AudioClip deathSound;
    public AudioClip deathSound2;

    public DamageNumber damageNumber;
    
    float distance = 0f;

    float xpSpawnChance;
    float foodSpawnChance;

    float foodSpawn;
    float xpSpawn;

    Vector2 spawnOffset;
    Vector2 spawnPosition;
    

    void Awake()
    {
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//find gamemanager
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();//find Player     
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>(); 

        Manager.enemyList.Add(gameObject);//add self to enemy list

        health = stats.health;//get stats from enemy stats
        damage = stats.damage;//get stats from enemy stats
        moveSpeed = stats.moveSpeed;//get stats from enemy stats
        attackRange = stats.attackRange;//get stats from enemy stats
        hungerProvided = stats.hungerProvided;//get stats from enemy stats

        xpSpawnChance = stats.xpSpawnChance;
        foodSpawnChance = stats.foodSpawnChance;

        deathEffect = stats.deathEffect;

        moveSpeed += Random.Range(-0.5f,0.5f);// for randomisation of move speed to ensure enemies dont clump together
    }


    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);//distance between instance transform and player
        //Moving towards the player, made method so can be changed in children enemies if necessary (dash and constant movement types?)
        Movement();

        Attack();

        //Attempting to attack the player if in range of them
        //if (Vector2.Distance(player.transform.position, transform.position) <= attackRange) { Attack(); }
    }

    //Default movement, enemy consistently moves towards player
    public void Movement()
    {
        float step = moveSpeed * Time.deltaTime;

        // move sprite towards the target location
        if(distance >= attackRange)
        {
            Vector2 moveVector = Vector2.MoveTowards(transform.position, player.transform.position, step);
            Vector2 positionVector = new Vector2(player.transform.position.x,player.transform.position.y );

            Vector2 velocityVector = moveVector - positionVector;
            if(velocityVector.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            if(velocityVector.x < 0)
            {
                spriteRenderer.flipX = true;
            }

            transform.position = moveVector;
        }
            
    }

    public void Attack()
    {
        
        if(distance <= attackRange)
        {
            player.takeDamage(damage);
        }
    }

    //public abstract void Attack();

    public void Die()
    {
        int deathEffectRandomiser = Random.Range(0, 1000); //Random second death sound as an easter egg

        if (deathEffectRandomiser == 666)//Asher if you see this Xav thinks that using 666 here is clever
        {
            deathEffectSound.clip = deathSound2;
            deathEffectSound.volume = 0.35f;
        }
        else { deathEffectSound.clip = deathSound; }

        //Create a death effect at the location of the enemy when they die
        Instantiate(deathEffect, transform.position, transform.rotation);
        Manager.enemyList.Remove(gameObject);
        Destroy(this.gameObject);

        xpSpawn = Random.Range(0f,100f);
        

        if(xpSpawn <= xpSpawnChance)
        {
            spawnOffset = new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f));
            spawnPosition = transform.position;
            spawnPosition += spawnOffset;
            Instantiate(Manager.expDrop, spawnPosition, Quaternion.identity);
        }

        foodSpawn = Random.Range(0f,100f);

        if(foodSpawn <= foodSpawnChance)
        {
            spawnOffset = new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f));
            spawnPosition = transform.position;
            spawnPosition += spawnOffset;
            GameObject FoodDrop = Instantiate(Manager.foodDrop, spawnPosition, Quaternion.identity);
            PickUp food = FoodDrop.GetComponent<PickUp>();
            food.Value = hungerProvided;
            //player.AddHunger(hungerProvided);
        }

    }

    public void TakeDamage(float damage)
    {
        int randomGore = Random.Range(0,Manager.goreList.Count);
        Instantiate(Manager.goreList[randomGore],transform.position,transform.rotation);
        print("BLOOOD");

        health -= damage;
        damageNumber.gameObject.SetActive(true);
        damageNumber.damageAmount = damage;
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
