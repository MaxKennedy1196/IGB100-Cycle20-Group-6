using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    GameManager Manager;
    public Player player;

    public GameObject moveMessage;
    public GameObject attackMessage;
    public GameObject hungerMessage;
    public GameObject xpMessage;

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
        switch (step)
        {
            case 0: TrackMovement(); break;
            case 1: CheckTentacleAttack(); break;
            case 2: CheckHungerPickup(); break;
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

            attackMessage.SetActive(false);
            hungerMessage.SetActive(true);
            step++;
        }
    }

    public void CheckHungerPickup()
    {
        if (!hasPickedUpHunger && step == 2)
        {
            hasPickedUpHunger = true;

            hungerMessage.SetActive(false);
            xpMessage.SetActive(true);
            step++;
        }
    }

    public void CheckXPPickup()
    {
        if (step == 3)
        {
            if (player.experience >= 20)
            {
                hasPickedUpXP = true;

                xpMessage.SetActive(false);
                step++;
            }
        }   
    }

    void TutorialComplete()
    {
        if (hasTentacleAttacked && hasPickedUpHunger && hasPickedUpXP)
        {
            Manager.tutorialComplete = true;
            Manager.screenFade.fadeCurve = Manager.startCurve;
            Manager.screenFade.ActivateFade();
            Destroy(gameObject);
        }
    }
}
