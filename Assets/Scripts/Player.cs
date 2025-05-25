using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameManager Manager;
    public float health = 100f;
    public float maxHealth = 100f;
    public float hunger = 100f;
    public float maxHunger = 100f;
    public float experience = 0f;
    public float experienceMult = 1.0f; //Used for experience boosting upgrades
    public float critMult = 2.0f; //Used for crit multiplier upgrades
    [HideInInspector] public float maxExperience;
    public SpriteRenderer spriteRenderer;
    public Animator hungeranimator;

    //Level logic
    public int level = 0; //Tracks the player's level
    public int winLevel = 20; //The level the player wins the game at
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

    private float hungerTimer;
    float starvationDamge = 2.5f;

    public List<AttackStats> AttackStatsList; //A list of all the players current weapons
    public AttackStats[] allAttacks; //Array of all the possible attacks to ensure upgrades reset on start
    public int maxWeapons; //Defines the maximum number of weapons the player can have

    public Transform target;

    public float distance;
    public float closestDistance = 999f;

    public List<Upgrade> upgrades;

    public EnemySpawner[] spawners;

    public Transform mouseTarget;

    [Header("Death Effect Variables")]
    public AnimationCurve zoomCurve;
    public GameObject deathEffect;
    public AnimationCurve fadeCurve;
    public CanvasGroup playerFade;
    public SceneFade screenFade;
    public GameObject[] hideUI;
    private bool dying = false;

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

        foreach(AttackStats attack in allAttacks)//go through each attck on the player
        {
            attack.ResetAttack();
            attack.upgradeTier = 0;
            attack.upgradeMaxed = false;
            attack.attackCooldown = attack.baseCooldown;
            attack.InitTimer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dying)
        {
            return;  
        }

        attacks();
        movement();
        hungerDecay();
        hungry();

        int currentLevel = Mathf.FloorToInt(level);
        if (currentLevel != lastCheckedLevel)
        {
            NextForm();
            lastCheckedLevel = currentLevel;
        }
    }

    private void attacks()
    {
        AttackStats[] AttackArray = AttackStatsList.ToArray();// fixed the error we were getting not sure if this will kneecap performance?
        foreach(AttackStats attack in AttackArray)//go through each attck on the player
        {
            attack.DecreaseTimer();//decrease attack timer on each attack on the player

            if(attack.attackTimer <= 0)//if attack timer is less than 0
            {
                GameObject projectileObject = Instantiate(attack.attackProjectile, transform.position, transform.rotation);//instantiate projectile
                Projectile projectile = projectileObject.GetComponent<Projectile>();// get projectile script

                if(attack.attackEffect != null)
                    Instantiate(attack.attackEffect, projectileObject.transform.position, projectileObject.transform.rotation);

                if(attack.targetingType == AttackStats.TargetingType.Closest)
                {
                    acquireClosestEnemy();
                }

                if(attack.targetingType == AttackStats.TargetingType.Random)
                {
                    acquireRandomEnemy();
                }

                if(attack.targetingType == AttackStats.TargetingType.Mouse_Pointer)
                {
                    target = mouseTarget.transform;
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
                projectile.damageMin = attack.attackMinDamage; //Using += to account for projectiles having damage upgrade amounts added to them
                projectile.damageMax = attack.attackMaxDamage; //Using += to account for projectiles having damage upgrade amounts added to them
                projectile.projectileLifetime = attack.attackLifetime;
                projectile.projectileSpeed = attack.attackSpeed;
                projectile.projectileArea = attack.attackArea;
                projectile.enemiesPassedThrough = attack.passthrough; //Using += to account for passthrough possibly being upgraded
                projectile.returnOnDeath = attack.returnOnDeath;
                projectile.critChance = attack.currentCritChance;

                projectile.GetCrit();

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
    

    public void takeDamage(float damage, bool aoeDamage = false)
    {
        if (aoeDamage) { health -= damage; }
        else { health -= damage * Time.deltaTime; }

        if (health <= 0)
        {
            StartCoroutine(DeathEffect());
            //SceneManager.LoadScene("Game Over");
            //Destroy(this.gameObject);
        }
    }

    private void hungerDecay()
    {
        hungerTimer += Time.deltaTime;

        if (hungerTimer >= 1.5f)
        {
            hunger -= 5f;
            hungerTimer = 0f;
        }

        if (hunger <= 0)
        {
            hunger = 0;
            takeDamage(starvationDamge);
        }
    }

    private void hungry()
    {
        if (hunger <= 20)
        {
            hungeranimator.SetBool("Hungry", true);
        }
        else
        {
            hungeranimator.SetBool("Hungry", false);
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
            Debug.Log($"{level}");
            if (level == winLevel) { Manager.GameWin(); }
            else
            {
                StartCoroutine(UpgradeMenu());

                if (playerForm < 4) { NextForm(); }
            }
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

    public void AddHealth(float amount)
    {
        health += amount;
        if (health >= maxHealth)
        {
            health = maxHealth;
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

    private IEnumerator DeathEffect()
    {
        dying = true;
        foreach (GameObject hideObject in hideUI) { hideObject.SetActive(false); }

        GameObject[] enemies = Manager.enemyList.ToArray();
        
        float zoomTime = 0.0f;
        Camera camera = Camera.main;

        while (zoomTime < 3f) //Zooming in on the player for dramatic effect
        {
            camera.orthographicSize = zoomCurve.Evaluate(zoomTime);
            playerFade.alpha = fadeCurve.Evaluate(zoomTime);
            zoomTime += Time.deltaTime;
            if (zoomTime > 1.5f) { foreach (GameObject enemy in enemies) { Destroy(enemy); } } //Destroy all enemies
            yield return null;
        }

        foreach (Form form in playerForms) { form.formObject.SetActive(false); }
        deathEffect.SetActive(true);

        screenFade.fadeCurve = fadeCurve;
        screenFade.ActivateFade();
        yield return new WaitForSeconds(screenFade.fadeDuration + screenFade.endWait);
        SceneManager.LoadScene(2);
    }
}