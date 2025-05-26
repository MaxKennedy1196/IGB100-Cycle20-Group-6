using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    GameManager Manager;
    public Player player;
    public PickUp pickUpInstance;
    private int foodpickup = 0;

    public GameObject moveMessage;
    public GameObject attackMessage;
    public GameObject hungerMessage;
    public GameObject xpMessage;
    public GameObject tutorialCompleteMessage;

    public Transform XPSpawn;
    public Transform FoodSpawn;

    private bool wPressed, aPressed, sPressed, dPressed;
    private bool hasTentacleAttacked = false;// I think the name of this variable is hilarious
    private bool hasPickedUpHunger = false;
    private bool hasPickedUpXP = false;

    private int step = 0;

    void Start()
    {
        moveMessage.SetActive(true);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();//find Player 
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//find gamemanager    
    }

    void Update()
    {
        if (player.experience >= 10)
        {
            player.experience = 10;
        }
        
        if (player.hunger <= 0)
        {
            SceneManager.LoadScene(1);
        }

        switch (step)
        {
            case 0: TrackMovement(); break;
            case 1: CheckTentacleAttack(); break;
            case 2: OnTutorialFoodPickedUp(); break;
            case 3: CheckXPPickup(); break;
        }

        TutorialComplete();
    }

    void TrackMovement()
    {
        if (Input.GetKeyDown(KeyCode.W)) wPressed = true;
        if (Input.GetKeyDown(KeyCode.A)) aPressed = true;
        if (Input.GetKeyDown(KeyCode.S)) sPressed = true;
        if (Input.GetKeyDown(KeyCode.D)) dPressed = true;

        if (wPressed && aPressed && sPressed && dPressed)
        {
            //player.movementComplete = true;
            moveMessage.SetActive(false);
            attackMessage.SetActive(true);
            step++;
        }
    }

    public void CheckTentacleAttack()
    {
        if (Manager.enemyCount == 0)
        {
            hasTentacleAttacked = true;
            Instantiate(Manager.expDrop, XPSpawn.position, XPSpawn.rotation);
            Instantiate(Manager.foodDrop, FoodSpawn.position, FoodSpawn.rotation);
            attackMessage.SetActive(false);
            hungerMessage.SetActive(true);
            step++;
        }
    }

    void OnTutorialFoodPickedUp()
    {
        if (foodpickup >= 1)
        {
            hasPickedUpHunger = true;

            hungerMessage.SetActive(false);
            xpMessage.SetActive(true);
            step++;
        }
        else
        {
            Debug.LogWarning("PickUp instance not found or foodpickup is less than 1.");
        }
    }

    public void CheckXPPickup()
    {

        if (player.experience != 0)
        {
            hasPickedUpXP = true;

            xpMessage.SetActive(false);
            tutorialCompleteMessage.SetActive(true);
            step++;

            StartCoroutine(WaitAndCompleteTutorial());
        }  
    }

    void TutorialComplete()
    {
        if (Manager.tutorialComplete)
        {
            Destroy(gameObject);
        }
    }

    private System.Collections.IEnumerator WaitAndCompleteTutorial()
    {
        yield return new WaitForSeconds(3f);
        tutorialCompleteMessage.SetActive(false);
        Manager.tutorialComplete = true;
        Manager.TutorialComplete();
    }

    public void OnFoodPickedUp()
    {
        foodpickup++;
        Debug.Log("Food Pickup Count (from TutorialManager): " + foodpickup);
    }
}
