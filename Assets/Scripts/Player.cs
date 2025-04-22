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


    float moveSpeed = 5f;
    Vector2 moveVector = new Vector2(0,0);
    float xInput;
    float yInput;

    public float damageMult = 1.0f;

    private bool isdecaying = false;

    private float hungerDecayRate = 1f;
    private float hungerDecayTimer = 0f;


    //[HideInInspector] public float timer;

    
    public List<AttackStats> AttackStatsList;
    public List<float> AttackTimers = new List<float>();
    List<float> newAttackTimers = new List<float>();

    public Transform target;

    float             eldritchLaserCooldown = 1f;
    float             eldritchLaserTimer = 1f;
    float             eldritchLaserLifetime = 5f;
    float             eldritchLaserDamage = 5f;
    float             eldritchLaserSpeed = 10f;
    float             eldritchLaserArea = 1f;
    public GameObject eldritchLaserEffect;
    public GameObject eldritchLaserProjectile;

    public float distance;
    public float closestDistance = 999f;

    void Awake()
    {
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//find gamemanager
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //foreach(AttackStats attack in AttackStatsList)
        //{
        //    AttackTimers.Add(attack.attackCooldown);
        //}
    }

    // Update is called once per frame
    void Update()
    {


        foreach(AttackStats attack in AttackStatsList)
        {
            attack.decreseTimer();

            if(attack.attackTimer <= 0)
            {
                GameObject projectileObject = Instantiate(eldritchLaserProjectile, transform.position, transform.rotation);
                Projectile projectile = projectileObject.GetComponent<Projectile>();
                projectile.target = target;
                projectile.damage = attack.attackDamage;
                projectile.projectileLifetime = attack.attackLifetime;
                projectile.projectileSpeed = attack.attackSpeed;
                projectile.projectileArea = attack.attackArea;


                attack.resetTimer();
                
            }
        }

        AttackTimers = newAttackTimers;
        newAttackTimers = new List<float>();


        closestDistance = 999f;
        Movement();
        hungerDecay();

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

    private void Movement()
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
        }
        if (Input.GetKey(KeyCode.A))
        {
            xInput = -1f;
        }

        moveVector = new Vector2(xInput, yInput);//put input in a 2D Vector
        moveVector = Vector3.Normalize(moveVector);// normalize 2D vector

        transform.Translate(moveVector * moveSpeed * Time.deltaTime);//move player
    }

    public void takeDamage(float damage)
    {
        health -= damage;

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
}
