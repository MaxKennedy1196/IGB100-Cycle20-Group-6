using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using System.Collections;

public class Player : MonoBehaviour
{
    public GameManager Manager;
    public float health = 100f;
    public float maxHealth = 100f;
    public float hunger = 100f;
    public float maxHunger = 100f;
    public float experience = 0f;
    public float experienceMult = 1.0f; //Used for experience boosting upgrades
    [HideInInspector] public float maxExperience;
    public SpriteRenderer spriteRenderer;

    //Level logic
    public int level = 0; //Tracks the player's level
    public float[] levelUpAmounts; //Contains the experience amounts required to progress to the next level

    [Serializable]
    public struct Form
    {
        public GameObject formObject; //Contains a form of the player
        public int formChangeLevel; //Contains the level that cause the player to change to the next form
    }

    [HideInInspector] public int playerForm;
    [SerializeField] public Form[] playerForms;


    public UpgradeMenu upgradeMenu;

    private int lastCheckedLevel = -1;

    public float moveSpeed = 5f;
    Vector2 moveVector = new Vector2(0,0);
    float xInput;
    float yInput;

    public float damageMult = 1.0f;

    private float hungerDecayRate = 5f;
    float starvationDamge = 2.5f;

    public List<AttackStats> AttackStatsList; //A list of all the players weapons
    public int maxWeapons; //Defines the maximum number of weapons the player can have

    public Transform target;

    public float distance;
    public float closestDistance = 999f;

    public List<Upgrade> upgrades;

    void Awake()
    {
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//find gamemanager
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        experience = 0f;
        maxExperience = levelUpAmounts[0];
        NextForm();

        foreach(AttackStats attack in AttackStatsList)//go through each attck on the player
        {
            attack.upgradeTier = 0;
            attack.upgradeMaxed = false;
            attack.attackCooldown = attack.baseCooldown;
            attack.InitTimer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        attacks();
        movement();
        hungerDecay();

        int currentLevel = Mathf.FloorToInt(level);
        if (currentLevel != lastCheckedLevel)
        {
            NextForm();
            lastCheckedLevel = currentLevel;
        }

    }

    private void attacks()
    {
        foreach(AttackStats attack in AttackStatsList)//go through each attck on the player
        {
            attack.DecreaseTimer();//decrease attack timer on each attack on the player

            if(attack.attackTimer <= 0)//if attack timer is less than 0
            {
                //Need to add logic for multiple projectiles if we are going to allow the projectile count to be upgraded

                GameObject projectileObject = Instantiate(attack.attackProjectile, transform.position, transform.rotation);//instantiate projectile
                Projectile projectile = projectileObject.GetComponent<Projectile>();// get projectile script
                

                if(attack.targetingType == AttackStats.TargetingType.Closest)
                {
                    acquireClosestEnemy();
                }

                if(attack.targetingType == AttackStats.TargetingType.Random)
                {
                    acquireRandomEnemy();
                }

                if(attack.targetingType == AttackStats.TargetingType.Player)
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
                projectile.damageMin += attack.attackMinDamage; //Using += to account for projectiles having damage upgrade amounts added to them
                projectile.damageMax += attack.attackMaxDamage; //Using += to account for projectiles having damage upgrade amounts added to them
                projectile.projectileLifetime += attack.attackLifetime;
                projectile.projectileSpeed = attack.attackSpeed;
                projectile.projectileArea += attack.attackArea; //Using += to account for projectiles having area upgrade amounts added to them
                projectile.enemiesPassedThrough += attack.passthrough; //Using += to account for passthrough possibly being upgraded


                attack.ResetTimer();
                
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
            takeDamage(starvationDamge);
        }
    }

    bool UpgradeSelection() { return upgradeMenu.upgradeSelected; } //Returns false until an upgrade is selected

    public IEnumerator UpgradeMenu()
    {
        Time.timeScale = 0.0f;
        upgradeMenu.gameObject.SetActive(true);
        yield return new WaitUntil(UpgradeSelection);
    }

    public void AddExperience(float amount)
    {
        experience += (amount * experienceMult);
        if (experience >= maxExperience)
        {
            experience = 0f;
            maxExperience = levelUpAmounts[level];
            level++;
            Debug.Log("Upgrade Selection Menu");
            StartCoroutine(UpgradeMenu());

            if (playerForm < 4) { NextForm(); }
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
        int randEnemy = UnityEngine.Random.Range(0, Manager.enemyList.Count + 1);
        target = Manager.enemyList[randEnemy].transform;
    }

    private void NextForm()
    {
        //Looping through all the forms and setting them as inactive
        foreach (Form form in playerForms)
        {
            form.formObject.SetActive(false);
        }

        //Setting the player's form based on their current level
        if (level < playerForms[0].formChangeLevel)
        {
            playerForms[0].formObject.SetActive(true);
            spriteRenderer = playerForms[0].formObject.GetComponent<SpriteRenderer>();
            playerForm = 1;
        }
        else if (level >= playerForms[0].formChangeLevel && level < playerForms[1].formChangeLevel)
        {
            playerForms[1].formObject.SetActive(true);
            spriteRenderer = playerForms[1].formObject.GetComponent<SpriteRenderer>();
            playerForm = 2;
        }
        else if (level >= playerForms[1].formChangeLevel && level < playerForms[2].formChangeLevel)
        {
            playerForms[2].formObject.SetActive(true);
            spriteRenderer = playerForms[2].formObject.GetComponent<SpriteRenderer>();
            playerForm = 3;
        }
        else if (level >= playerForms[3].formChangeLevel)
        {
            playerForms[3].formObject.SetActive(true);
            spriteRenderer = playerForms[3].formObject.GetComponent<SpriteRenderer>();
            playerForm = 4;
        }
    }
}
