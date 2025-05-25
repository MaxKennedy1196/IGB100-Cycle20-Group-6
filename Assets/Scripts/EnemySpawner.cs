using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameManager Manager;

    //public float enemyRate;
    //public float enemyTimer;
    //public GameObject Enemy;
    public bool spawningOn;
    private Player player;


    
    //public GameObject orbPrefab;

    private float farmerSpawnRate = 3.0f;
    private float farmerspawnTimer;
    private float blacksmithSpawnRate = 12f;
    private float blacksmithspawnTimer;
    private float clecricSpawnRate = 10f;
    private float clericspawnTimer;

    int maxFarmers = 250;
    int maxBlacksmiths = 100;
    int maxClerics = 50;



    
    void Start()
    {
        player = Manager.player;
    }


    // Update is called once per frame
    void Update()
    {
        if(spawningOn && Manager.enemyList.Count < 350)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if(Time.time > farmerspawnTimer && Manager.farmerCount <= maxFarmers)
        {
            Instantiate(Manager.farmerPrefab, transform.position, Quaternion.identity);
            farmerspawnTimer = Time.time + farmerSpawnRate + Random.Range(-1f,1f);
        }    

        if(player.level >= 1)
        {
            farmerSpawnRate = 4f;
            blacksmithSpawnRate = 12f;
            clecricSpawnRate = 10f;

        }
        if(player.level >= 2)
        {
            farmerSpawnRate = 2.5f;
            blacksmithSpawnRate = 12f;
            clecricSpawnRate = 10f;
        }
        if(player.level >= 3)
        {
            farmerSpawnRate = 2.25f;
            blacksmithSpawnRate = 12f;
            clecricSpawnRate = 9f;

        }
        if(player.level >= 4)
        {
            farmerSpawnRate = 2f;
            blacksmithSpawnRate = 12f;
            clecricSpawnRate = 8f;

            
        }
        if(player.level >= 5)
        {
            farmerSpawnRate = 1.75f;
            blacksmithSpawnRate = 12f;
            clecricSpawnRate = 7f;

            if(Time.time > blacksmithspawnTimer && Manager.blacksmithCount <= maxBlacksmiths)
            {
                Instantiate(Manager.blacksmithPrefab, transform.position, Quaternion.identity);
                blacksmithspawnTimer = Time.time + blacksmithSpawnRate + Random.Range(-1f,1f);
            }
        }
        if(player.level >= 6)
        {
            farmerSpawnRate = 1.5f;
            blacksmithSpawnRate = 8f;
            clecricSpawnRate = 6f;
        }
        if(player.level >= 7)
        {
            farmerSpawnRate = 1.25f;
            blacksmithSpawnRate = 6f;
            clecricSpawnRate = 5f;
            

        }
        if(player.level >= 8)
        {
            farmerSpawnRate = 1.25f;
            blacksmithSpawnRate = 4f;
            clecricSpawnRate = 4f;
        }
        if(player.level >= 9)
        {
            farmerSpawnRate = 1.25f;
            blacksmithSpawnRate = 4f;
            clecricSpawnRate = 4f;

            if(Time.time > clericspawnTimer && Manager.clericCount <= maxClerics)
            {
                Instantiate(Manager.clericPrefab, transform.position, Quaternion.identity);
                clericspawnTimer = Time.time + clecricSpawnRate + Random.Range(-1f,1f);
            }
        }
        if(player.level >= 10)
        {
            farmerSpawnRate = 1f;
            blacksmithSpawnRate = 4f;
            clecricSpawnRate = 4f;
        }
        if(player.level >= 13)
        {
            farmerSpawnRate = .5f;
            blacksmithSpawnRate = 4f;
            clecricSpawnRate = 4f;
        }
        if(player.level >= 15)
        {
            farmerSpawnRate = 0.25f;
            blacksmithSpawnRate = 3f;
            clecricSpawnRate = 3f;
        }
        if(player.level >= 17)
        {
            farmerSpawnRate = 0.125f;
            blacksmithSpawnRate = 2f;
            clecricSpawnRate = 2f;
        }
        if(player.level >= 20)
        {
            farmerSpawnRate = 0.125f;
            blacksmithSpawnRate = 2f;
            clecricSpawnRate = 2f;
        }
    }
}
