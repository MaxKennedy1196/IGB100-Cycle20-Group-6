using UnityEngine;

public class PickUp : MonoBehaviour
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

        distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance <= pickupDistance)
        {
            if(pickupType == PickupType.XP)
            {
                player.AddExperience(Value);
                AudioSource.PlayClipAtPoint(pickUpSound, transform.position);

                if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1)
                {
                    FindObjectOfType<TutorialManager>()?.CheckXPPickup();
                }

                Destroy(this.gameObject);
            }

            if(pickupType == PickupType.Food)
            {
                player.AddHunger(Value);
                AudioSource.PlayClipAtPoint(pickUpSound, transform.position);

                if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1)
                {
                    FindObjectOfType<TutorialManager>()?.CheckHungerPickup();
                }

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
