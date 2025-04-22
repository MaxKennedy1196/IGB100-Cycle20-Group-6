using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameManager Manager;
    public float damage;

    //Internal boolean to check if the projectile has dealt damage to an enemy
    private bool damageDealt = false;

    [HideInInspector] public float projectileSpeed;
    [HideInInspector] public float projectileLifetime;
    [HideInInspector] public float projectileArea;
    [HideInInspector] public int enemiesPassedThrough;
    
    public Transform target;
    public Enemy targetStats;

    public float distance;
    public float closestDistance = 999f;

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
        transform.position += transform.up * Time.deltaTime * projectileSpeed;

        foreach(GameObject enemy in Manager.enemyList)//target acquisition;
        { 
            distance = Vector3.Distance(transform.position, enemy.transform.position);//distance between instance transform and given enemy within enemy list

            if(distance <= projectileArea)//if this particular enemy is closer than all previous ones make it the new minimum distance
            {
                targetStats = enemy.GetComponent<Enemy>();
                targetStats.TakeDamage(damage);
                enemiesPassedThrough -= 1;
                if(enemiesPassedThrough <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
        }

    }
}
