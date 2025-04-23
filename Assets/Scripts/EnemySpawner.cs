using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public float enemyRate = 2.0f;
    public float enemyTimer;
    public GameObject Enemy;
    public bool spawningOn;
    

    // Update is called once per frame
    void Update()
    {
        if(spawningOn)
        {
            SpawnEnemy();
        }
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
