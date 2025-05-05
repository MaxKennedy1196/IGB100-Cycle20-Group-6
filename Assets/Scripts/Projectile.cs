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
    public int enemiesPassedThrough;
    
    public Transform target;
    public Enemy targetStats;

    public float distance;
    public float closestDistance = 999f;

    public bool bindToPlayer = false;
    public bool DPS = false;
    float damageToEnemy = 0f;

    void Awake()
    {
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//find gamemanager
    }

    public void Start()
    {
        Destroy(this.gameObject, projectileLifetime);

        Vector2 direction = target.position - transform.position;//Point towards target
        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);//Point towards target

    }

    public void Update()
    {
        if(bindToPlayer == false)
        {
            transform.position += transform.up * Time.deltaTime * projectileSpeed;
        }

        if(bindToPlayer == true)
        {
            transform.position = target.position;
        }
            
        GameObject[] enemyArray = Manager.enemyList.ToArray();// fixed the error we were getting not sure if this will kneecap performance?
        foreach(GameObject enemy in enemyArray)//target acquisition;
        { 
            distance = Vector3.Distance(transform.position, enemy.transform.position);//distance between instance transform and given enemy within enemy list

            if(distance <= projectileArea)//if this particular enemy is closer than all previous ones make it the new minimum distance
            {
                targetStats = enemy.GetComponent<Enemy>();
                damageToEnemy = Random.Range(damageMin,damageMax);

                if(DPS == true)
                {
                    damageToEnemy = damageToEnemy * Time.deltaTime;
                }

                targetStats.TakeDamage(damageToEnemy);
                enemiesPassedThrough -= 1;
                if(enemiesPassedThrough <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
        }

    }
}
