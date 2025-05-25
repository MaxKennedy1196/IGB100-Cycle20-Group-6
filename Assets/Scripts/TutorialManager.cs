using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public TutorialPlayer player;

    public GameObject moveMessage;
    public GameObject attackMessage;
    public GameObject hungerMessage;
    public GameObject xpMessage;

    private bool wPressed, aPressed, sPressed, dPressed;
    private bool hasTentacleAttacked = false;
    private bool hasPickedUpHunger = false;
    private bool hasPickedUpXP = false;

    private int step = 0;

    void Start()
    {
        moveMessage.SetActive(true);
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
    }

    void TrackMovement()
    {
        if (Input.GetKeyDown(KeyCode.W)) wPressed = true;
        if (Input.GetKeyDown(KeyCode.A)) aPressed = true;
        if (Input.GetKeyDown(KeyCode.S)) sPressed = true;
        if (Input.GetKeyDown(KeyCode.D)) dPressed = true;

        if (wPressed && aPressed && sPressed && dPressed)
        {
            player.movementComplete = true;
            moveMessage.SetActive(false);
            attackMessage.SetActive(true);
            step++;
        }
    }

    public void CheckTentacleAttack()
    {
        // check if the player has attacked with the tentacle
        
    }

    public void CheckHungerPickup()
    {
        player.hunger = 80;
        if (player.hunger > 80)
        {
            hasPickedUpHunger = true;
            hungerMessage.SetActive(false);
            step++;
        }
    }

    public void CheckXPPickup()
    {
        if (!hasPickedUpXP && step == 3)
        {
            if (player.experience >= 10) // Assuming 10 is the required XP for this step
            {
                hasPickedUpXP = true;
                xpMessage.SetActive(false);
                step++;
            }
        }   
    }
}
