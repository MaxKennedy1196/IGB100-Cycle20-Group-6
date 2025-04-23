using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

//Script used to handle game elements and player upgrades
public class GameManager : MonoBehaviour
{
    //Singleton Setup
    public static GameManager instance = null;

    public float volumeLevel;

    //public GameObject tentacleObject;

    public List<GameObject> enemyList = new List<GameObject>();
    public Player player;

    public GameObject expDrop;
    public GameObject foodDrop;
    

    // Awake Checks - Singleton setup
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();//find Player            
    }

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) { TentacleIncrease(); }
    }

    /*public void TentacleIncrease()
    {
        GameObject newTentacle = Instantiate(tentacleObject, player.transform.position, player.transform.rotation, player.transform);
    }*/
}
