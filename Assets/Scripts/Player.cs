using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    public GameManager Manager;
    public float health = 100f;
    public float maxHealth = 100f;
    public float hunger = 100f;
    public float maxHunger = 100f;
    public float experience = 100f;
    public float maxExperience = 100f;
    public SpriteRenderer spriteRenderer;

    public float level = 1f;


    float moveSpeed = 5f;
    Vector2 moveVector = new Vector2(0,0);
    float xInput;
    float yInput;

    public float damageMult = 1.0f;

    private bool isdecaying = false;

    private float hungerDecayRate = 1.5f;
    private float hungerDecayTimer = 0f;


    //[HideInInspector] public float timer;

    
    public List<AttackStats> AttackStatsList;

    public Transform target;

    public float distance;
    public float closestDistance = 999f;

    void Awake()
    {
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//find gamemanager
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>(); 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(AttackStats attack in AttackStatsList)//go through each attck on the player
        {
            attack.initTimer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        attacks();
        movement();
        hungerDecay();
        
    }

    private void attacks()
    {
        foreach(AttackStats attack in AttackStatsList)//go through each attck on the player
        {
            attack.decreseTimer();//decrease attack timer on each attack on the player

            if(attack.attackTimer <= 0)//if attack timer is less than 0
            {
                GameObject projectileObject = Instantiate(attack.attackProjectile, transform.position, transform.rotation);//instantiate projectile
                Projectile projectile = projectileObject.GetComponent<Projectile>();// get projectile script
                

                if(attack.targettingType == AttackStats.TargettingType.Closest)
                {
                    acquireClosestEnemy();
                }

                if(attack.targettingType == AttackStats.TargettingType.Random)
                {
                    acquireRandomEnemy();
                }

                if(attack.targettingType == AttackStats.TargettingType.Player)
                {
                    target = gameObject.transform;
                    projectile.bindToPlayer = true;
                }

                if(attack.DPS == true)
                {
                    projectile.DPS = true;
                }   
                if(attack.DPS == false)
                {
                    projectile.DPS = false;
                }             
                    
                projectile.target = target;//allocate projectile target
                projectile.damageMin = attack.attackMinDamage;
                projectile.damageMax = attack.attackMaxDamage;
                projectile.projectileLifetime = attack.attackLifetime;
                projectile.projectileSpeed = attack.attackSpeed;
                projectile.projectileArea = attack.attackArea;
                projectile.enemiesPassedThrough = attack.passthrough;


                attack.resetTimer();
                
            }
        }

        closestDistance = 999f;
    }

    private void movement()
    {
        yInput = 0;
        xInput = 0;

        //get input for movement
        if (Input.GetKey(KeyCode.W))
        {
            yInput = 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            yInput = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            xInput = 1f;
            spriteRenderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            xInput = -1f;
            spriteRenderer.flipX = false;
        }

        moveVector = new Vector2(xInput, yInput);//put input in a 2D Vector
        moveVector = Vector3.Normalize(moveVector);// normalize 2D vector

        transform.Translate(moveVector * moveSpeed * Time.deltaTime);//move player
    }

    public void takeDamage(float damage)
    {
        health -= damage * Time.deltaTime;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void hungerDecay()
    {
        hunger -= hungerDecayRate * Time.deltaTime;

        if (hunger <= 0)
        {
            hunger = 0;
            takeDamage(5f * Time.deltaTime);
        }
    }

    public void AddExperience(float amount)
    {
        experience += amount;
        if (experience >= maxExperience)
        {
            experience = 0;
            maxExperience += 10;
            level += 1;

            //call powerup cards funtion here
        }
    }
    
    public void AddHunger(float amount)
    {
        hunger += amount;
        if (hunger >= maxHunger)
        {
            hunger = maxHunger;
        }
    }

    private void acquireClosestEnemy()
    {
        foreach(GameObject enemy in Manager.enemyList)//target acquisition;
        { 
            distance = Vector3.Distance(transform.position, enemy.transform.position);//distance between instance transform and given enemy within enemy list
    
            if(distance < closestDistance)//if this particular enemy is closer than all previous ones make it the new minimum distance
            {
                closestDistance = distance;
                target = enemy.transform; // set closest enemy to target
            }
        }
    }

    private void acquireRandomEnemy()
    {
        int randEnemy = Random.Range(0, Manager.enemyList.Count + 1);
        target = Manager.enemyList[randEnemy].transform;
    }

}
