using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

//Abstract enemy parent class to be inherited from for child enemy types 
public class Enemy : MonoBehaviour
{
    GameManager Manager;
    Player player;

    public EnemyStats stats;

    [HideInInspector] public float health;
    [HideInInspector] public float damage;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public float attackRange;
    [HideInInspector] public float hungerProvided;

    public SpriteRenderer spriteRenderer;

    [Header("Death/Damage Effects")]
    public GameObject deathEffect;
    public AudioSource deathEffectSound;
    public AudioClip deathSound;
    public AudioClip deathSound2;
    public DamageNumber damageNumber;

    [Header("Cleric Variables")]
    public GameObject enemyAttack; //AOE if the enemy is a cleric, leave null otherwise
    public float aoeCooldown; //Sets how often the cleric enemy can leave an AOE on the ground
    float aoeTimer; //Tracks when the next AOE can be used

    float distance = 0f;

    float xpSpawnChance;
    float foodSpawnChance;
    float hpSpawnChance;

    float foodSpawn;
    float xpSpawn;
    float hpSpawn;

    Vector2 spawnOffset;
    Vector2 spawnPosition;

    string Name = "";

    public Rigidbody2D rb;

    Vector3 startFramePos;

    void Awake()
    {
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//find gamemanager
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();//find Player     
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();

        Name = stats.Name;

        if (Name == "Farmer")
        {
            Manager.farmerCount += 1;
        }
        if (Name == "Blacksmith")
        {
            Manager.blacksmithCount += 1;
        }
        if (Name == "Cleric")
        {
            Manager.clericCount += 1;
        }

        Manager.enemyList.Add(gameObject);//add self to enemy list

        health = stats.health;//get stats from enemy stats
        damage = stats.damage;//get stats from enemy stats
        moveSpeed = stats.moveSpeed;//get stats from enemy stats
        attackRange = stats.attackRange;//get stats from enemy stats
        hungerProvided = stats.hungerProvided;//get stats from enemy stats

        xpSpawnChance = stats.xpSpawnChance;
        foodSpawnChance = stats.foodSpawnChance;
        hpSpawnChance = stats.healthSpawnChance;

        deathEffect = stats.deathEffect;

        moveSpeed += Random.Range(-0.5f, 0.5f);// for randomisation of move speed to ensure enemies dont clump together

        aoeTimer = Time.time + aoeCooldown;
        
        rb = GetComponent<Rigidbody2D>();
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
        Vector3 direction = (player.transform.position - transform.position).normalized;
        if (direction.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        if(direction.x > 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void FixedUpdate()
    {
        if (distance >= attackRange)
        {
            // Calculate the direction towards the player
            Vector3 direction = (player.transform.position - transform.position).normalized;

            // Calculate the new position using MoveTowards
            Vector3 newPosition = Vector3.MoveTowards(transform.position, transform.position + direction * moveSpeed * Time.fixedDeltaTime, moveSpeed * Time.fixedDeltaTime);

            // Move the Rigidbody to the new position
            rb.MovePosition(newPosition);


            
        }
    }

    public void Attack()
    {
        if (enemyAttack != null && Time.time > aoeTimer) //If the enemy has an attack and their attack timer is less than the current time, letting them attack
        {
            Instantiate(enemyAttack, transform.position, transform.rotation);
            aoeTimer = Time.time + aoeCooldown;
        }

        if (distance <= attackRange) //Needs to be changed if we want the cleric to not attack normally
        {
            player.takeDamage(damage);
        }
    }

    //public abstract void Attack();

    public void Die()
    {
        int deathEffectRandomiser = Random.Range(0, 1000); //Random second death sound as an easter egg

        if (deathEffectRandomiser == 666)//Asher if you see this Xav thinks that using 666 here is clever //Xav I do see this and I completely agree
        {
            deathEffectSound.clip = deathSound2;
            deathEffectSound.volume = 0.35f;
        }
        else { deathEffectSound.clip = deathSound; }

        if (Name == "Farmer")
        {
            Manager.farmerCount -= 1;
        }
        if (Name == "Blacksmith")
        {
            Manager.blacksmithCount -= 1;
        }
        if (Name == "Cleric")
        {
            Manager.clericCount -= 1;
        }

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

        hpSpawn = Random.Range(0f,100f);
        
        if(hpSpawn <= hpSpawnChance)
        {
            spawnOffset = new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f));
            spawnPosition = transform.position;
            spawnPosition += spawnOffset;
            Instantiate(Manager.hpDrop, spawnPosition, Quaternion.identity);
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

    public void TakeDamage(float damage, bool crit)
    {
        int randomGore = Random.Range(0,Manager.goreList.Count);
        int randomSound = Random.Range(0, Manager.enemyGoreSounds.Count);
        Instantiate(Manager.goreList[randomGore],transform.position,transform.rotation);
        Instantiate(Manager.enemyGoreSounds[randomSound],transform.position,transform.rotation);

        if (crit) { damage *= Manager.player.critMult; } //Multiplying damage by the current critical multiplier (defaults to 2x)
        health -= damage;
        damageNumber.gameObject.SetActive(true);
        damageNumber.CreatePopUp(transform.position, ((int)damage).ToString(), crit);
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
