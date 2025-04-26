using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameManager Manager;

    public float enemyRate;
    public float enemyTimer;
    public GameObject Enemy;
    public bool spawningOn;
    private Player player;
    
    void Start()
    {
        player = Manager.player;
    }


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
        if (Time.time > enemyTimer)
        {
            Instantiate(Enemy, transform.position, Quaternion.identity);
            enemyTimer = Time.time + enemyRate;
        }
    }
}
