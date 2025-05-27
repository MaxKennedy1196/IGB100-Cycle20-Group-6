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

    public Transform XPSpawn;
    public Transform[] FoodSpawn;

    private bool wPressed, aPressed, sPressed, dPressed;
    private bool hasTentacleAttacked = false; // I think the name of this variable is hilarious
    private bool hasPickedUpHunger = false;
    private bool hasPickedUpXP = false;

    private int step = 0;

    void Start()
    {
        moveMessage.SetActive(true);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); // find Player 
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>(); // find gamemanager    
    }

    void Update()
    {
        if (player.experience >= 12)
        {
            player.experience = 12;
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
        // Track player movement input be or statement

        if (Input.GetKeyDown(KeyCode.W))
        {
            wPressed = true;
            aPressed = true;
            sPressed = true;
            dPressed = true;
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            aPressed = true;
            wPressed = true;
            sPressed = true;
            dPressed = true;
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            sPressed = true;
            wPressed = true;
            aPressed = true;
            dPressed = true;
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            dPressed = true;
            wPressed = true;
            aPressed = true;
            sPressed = true;
        }

        if (wPressed && aPressed && sPressed && dPressed)
        {
            //player.movementComplete = true;
            StartCoroutine(WaitForSeconds(2f));
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

            foreach (Transform foodSpawnPoint in FoodSpawn)
            {
                Instantiate(Manager.foodDrop, foodSpawnPoint.position, foodSpawnPoint.rotation);
            }

            StartCoroutine(WaitForSeconds(1.5f));
            attackMessage.SetActive(false);
            hungerMessage.SetActive(true);
            StartCoroutine(WaitForSeconds(1f));
            step++;
        }
    }

    void OnTutorialFoodPickedUp()
    {
        if (foodpickup >= 4)
        {
            hasPickedUpHunger = true;

            StartCoroutine(WaitForSeconds(3f));
            hungerMessage.SetActive(false);
            StartCoroutine(WaitForSeconds(2f));
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

        if (player.experience >= 10)
        {
            hasPickedUpXP = true;

            StartCoroutine(WaitForSeconds(1f));
            xpMessage.SetActive(false);
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
        yield return new WaitForSeconds(1.5f);
        Manager.tutorialComplete = true;
        Manager.TutorialComplete();
    }

    private System.Collections.IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public void OnFoodPickedUp()
    {
        foodpickup++;
        Debug.Log("Food Pickup Count (from TutorialManager): " + foodpickup);
    }
}
