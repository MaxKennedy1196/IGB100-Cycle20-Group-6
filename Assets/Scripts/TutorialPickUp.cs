using UnityEngine;

public class TutorialPickUp : MonoBehaviour
{
    public float Value = 10;
    public AudioClip pickUpSound;
    public Player player;
    public Transform playerTransform;
    float pickupDistance = 1.5f;
    float distance = 0f;
    
    // item magnet stuff
    Rigidbody2D rb;
    bool hasTarget;
    Vector3 targetPosition;
    float moveSpeed = 5f;

    public PickupType pickupType;// What type of targetting does this attack use

    public enum PickupType
    {
        XP,
        HP,
        Food
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();//find Player     
        playerTransform = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        var tutorialManager = FindObjectOfType<TutorialManager>();

        distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance <= pickupDistance)
        {
            if(pickupType == PickupType.XP)
            {
                player.AddExperience(Value);
                tutorialManager?.CheckXPPickup();
                AudioSource.PlayClipAtPoint(pickUpSound, transform.position);
                Destroy(this.gameObject);
            }

            if(pickupType == PickupType.Food)
            {
                player.AddHunger(Value);
                tutorialManager?.CheckHungerPickup();
                AudioSource.PlayClipAtPoint(pickUpSound, transform.position);
                Destroy(this.gameObject);
            }

            if(pickupType == PickupType.HP)
            {
                player.AddHealth(Value);
                AudioSource.PlayClipAtPoint(pickUpSound, transform.position);
                Destroy(this.gameObject);
            }
                
        }
    }


    private void FixedUpdate()
    {
        if (hasTarget)
        {
            Vector2 targetDirection = (targetPosition - transform.position).normalized;
            rb.linearVelocity = new Vector2(targetDirection.x, targetDirection.y) * moveSpeed;
        }
    }

    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        hasTarget = true;
    }
}
