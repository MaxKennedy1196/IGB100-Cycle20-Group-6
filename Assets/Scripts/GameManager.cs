using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

//Script used to handle game elements and player upgrades
public class GameManager : MonoBehaviour
{
    //Singleton Setup
    public static GameManager instance = null;

    public float volumeLevel;

    //public GameObject tentacleObject;

    public List<GameObject> enemyList = new List<GameObject>();
    public Player player;
    public GameObject playerCamera;

    public GameObject expDrop;
    public GameObject foodDrop;

    public List<GameObject> goreList;
    

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

    }

    public void GameWin()
    {
        if (player.level == 20)
        {
            SceneManager.LoadScene("You Win");
        }
    }
}