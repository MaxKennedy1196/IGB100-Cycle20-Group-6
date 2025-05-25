using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameManager Manager;
    public float damageMin;
    public float damageMax;

    //Internal boolean to check if the projectile has dealt damage to an enemy
    private bool damageDealt = false;

    public float projectileSpeed;
    public float projectileLifetime;
    public float projectileArea;
    float projectileScale;
    public bool scale = false;
    public AnimationCurve scaleCurve;
    float curveTime = 0.0f;
    [HideInInspector] public int enemiesPassedThrough;
    [HideInInspector] public int critChance;
    private bool crit = false;
    //[HideInInspector] public int upgradedPassThrough;

    public Transform target;
    public Enemy targetStats;

    public float distance;
    public float closestDistance = 999f;

    public bool bindToPlayer = false;
    public bool DPS = false;
    float damageToEnemy = 0f;

    public bool returnOnDeath = false;

    public float TimeAlive = 0f;

    public bool returning;
    public Player player;

    List<GameObject> inRangeEnemies;

    Vector3 scaleVector;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); //Find Player
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>(); //Find Game Manager
    }

    public void Start()
    {
        projectileScale = projectileArea * 2;
        scaleVector = new Vector3(projectileScale, projectileScale, projectileScale);

        if (returnOnDeath == false)
        {
            Destroy(this.gameObject, projectileLifetime);
        }

        if (target != null)
        {
            Vector2 direction = target.position - transform.position;//Point towards target
            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);//Point towards target
        }
    }

    float DPSTimer = 0f;

    public void Update()
    {
        //Scale pulsing effect for Miasma
        if (scale)
        {
            if (curveTime > 4.0f) { curveTime = 0.0f; }
            transform.localScale = projectileScale * scaleCurve.Evaluate(curveTime) * Vector3.one;
            projectileArea = projectileScale/2 * scaleCurve.Evaluate(curveTime);
            curveTime += Time.deltaTime;
        }

        DPSTimer += Time.deltaTime;

        TimeAlive += Time.deltaTime;

        if (TimeAlive >= projectileLifetime)
        {
            returning = true;
        }

        if (bindToPlayer == false)
        {
            transform.position += transform.up * Time.deltaTime * projectileSpeed;
        }

        if (bindToPlayer == true)
        {
            transform.position = target.position;
        }

        if (returning == false)
        {
            List<GameObject> inRangeEnemies = new(); //A new list of all the enemies within range of the attack, could potentially facilitate proper closest vs random targeting

            GameObject[] enemyArray = Manager.enemyList.ToArray();
            foreach (GameObject enemy in enemyArray)
            {
                distance = Vector3.Distance(transform.position, enemy.transform.position); //Distance between instance transform and given enemy within enemy list

                if (distance <= projectileArea)
                {
                    inRangeEnemies.Add(enemy);
                }
            }

            if (DPS) //New implementation for dps attacks so they hit all enemies within range, rather than just one at random
            {
                if (DPSTimer >= 0.25f)
                {
                    foreach (GameObject damageEnemy in inRangeEnemies)
                    {
                        targetStats = damageEnemy.GetComponent<Enemy>();
                        damageToEnemy = Random.Range(damageMin, damageMax);
                        targetStats.TakeDamage(damageToEnemy, crit);
                    }

                    DPSTimer = 0f;
                }
            }

            else
            {
                int targetPosition = Random.Range(0, inRangeEnemies.Count); //Picking a random enemy from the enemies within attack range
                targetStats = inRangeEnemies[targetPosition].GetComponent<Enemy>(); //Setting target stats to the randomly chosen enemy
                Instantiate(Manager.dmgEffect, transform.position, transform.rotation); //Creating the attack effect
                damageToEnemy = Random.Range(damageMin, damageMax); //Randomising the damage amount and dealing damage
                targetStats.TakeDamage(damageToEnemy, crit);
            }

            enemiesPassedThrough -= 1;
            if (enemiesPassedThrough <= 0)
            {
                if (returnOnDeath == false)
                {
                    Destroy(this.gameObject);
                }
                if (returnOnDeath == true)
                {
                    returning = true;
                }
            }
        }

        else //Code for if projectile is returning (used for tentacle)
        {
            target = player.transform;
            Vector2 direction = target.position - transform.position;//Point towards target
            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);//Point towards target
            transform.position += transform.up * Time.deltaTime * projectileSpeed;

            float playerdistance = Vector3.Distance(transform.position, player.transform.position);

            if (playerdistance <= 0.5)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void GetCrit() //Crit randomisation
    {
        int critRandomiser = Random.Range(0, critChance+1);
        Debug.Log($"Crit chance is {critChance}, crit randomiser is {critRandomiser}");
        if (critRandomiser == 1) { crit = true; }
        else { crit = false; }
    }
}