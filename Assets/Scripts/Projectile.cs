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
    public float projectileScale;
    [HideInInspector] public int enemiesPassedThrough;
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

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); //Find Player
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>(); //Find Game Manager
    }

    public void Start()
    {
        transform.localScale += new Vector3(projectileScale, projectileScale, projectileScale);

        if (returnOnDeath == false)
        {
            Destroy(this.gameObject, projectileLifetime);
        }

        if(target != null)
        {
            Vector2 direction = target.position - transform.position;//Point towards target
            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);//Point towards target
        }
    }

    float DPSTimer = 0f;

    public void Update()
    {
        DPSTimer += Time.deltaTime;

        TimeAlive += Time.deltaTime;

        if(TimeAlive >= projectileLifetime)
        {
            returning = true;
        }

        if(bindToPlayer == false)
        {
            transform.position += transform.up * Time.deltaTime * projectileSpeed;
        }

        if(bindToPlayer == true)
        {
            transform.position = target.position;
        }

        if(returning == false)
        {
            /* Not functional yet, aiming to make DPS attacks function against all enemies within range
            List<GameObject> inRangeEnemies = new();

            GameObject[] enemyArray = Manager.enemyList.ToArray();
            foreach (GameObject enemy in enemyArray)
            {
                distance = Vector3.Distance(transform.position, enemy.transform.position); //Distance between instance transform and given enemy within enemy list

                if (distance <= projectileArea)
                {
                    inRangeEnemies.Add(enemy);
                }
            }

            */
            
            GameObject[] enemyArray = Manager.enemyList.ToArray();// fixed the error we were getting not sure if this will kneecap performance?
            foreach(GameObject enemy in enemyArray)//target acquisition;
            { 
                distance = Vector3.Distance(transform.position, enemy.transform.position);//distance between instance transform and given enemy within enemy list

                if(distance <= projectileArea)//if this particular enemy is closer than all previous ones make it the new minimum distance
                {
                    targetStats = enemy.GetComponent<Enemy>();
                    if(DPS == false)
                    {
                        Instantiate(Manager.dmgEffect, transform.position, transform.rotation);
                        damageToEnemy = Random.Range(damageMin,damageMax);
                        targetStats.TakeDamage(damageToEnemy);
                    }
                    if(DPS == true)
                    {   
                        if(DPSTimer >= 0.25f)
                        {
                            damageToEnemy = Random.Range(damageMin,damageMax);
                            targetStats.TakeDamage(damageToEnemy);
                            DPSTimer = 0f;
                        }
                    }
                
                
                    enemiesPassedThrough -= 1;
                    if(enemiesPassedThrough <= 0)
                    {
                        if(returnOnDeath == false)
                        {   
                            Destroy(this.gameObject);
                        }
                        if(returnOnDeath == true)
                        {   
                            returning = true;
                        }

                    }
                }
            }
        }

        if(returning == true)
        {
            target = player.transform;
            Vector2 direction = target.position - transform.position;//Point towards target
            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);//Point towards target
            transform.position += transform.up * Time.deltaTime * projectileSpeed;

            float playerdistance = Vector3.Distance(transform.position, player.transform.position);

            if(playerdistance <= 0.5)
            {
                Destroy(this.gameObject);
            }
        }

        

    }
}
