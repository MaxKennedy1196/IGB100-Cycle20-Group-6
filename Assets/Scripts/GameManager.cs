using Unity.VisualScripting;
using UnityEngine;

//Script used to handle game elements and player upgrades
public class GameManager : MonoBehaviour
{
    //Singleton Setup
    public static GameManager instance = null;

    public float volumeLevel;

    public PlayerMovement player;

    public GameObject tentacleObject;

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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { TentacleIncrease(); }
    }

    public void TentacleIncrease()
    {
        GameObject newTentacle = Instantiate(tentacleObject, player.transform.position, player.transform.rotation, player.transform);
    }
}
