using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public float enemyRate = 2.0f;
    public float enemyTimer;
    public GameObject Enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if(Time.time > enemyTimer)
        {
            Instantiate(Enemy, transform.position, Quaternion.identity);
            enemyTimer = Time.time + enemyRate;
        }
    }
}
