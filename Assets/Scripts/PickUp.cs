using UnityEngine;

public class PickUp : MonoBehaviour
{
    public float Value = 10;
    public AudioClip pickUpSound;
    public Player player;
    public Transform playerTransform;
    float pickupDistance = 1.5f;
    float distance = 0f;

    public PickupType pickupType;// What type of targetting does this attack use

    public enum PickupType
    {
        XP,
        HP,
        Food
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
                Destroy(this.gameObject);
            }

            if(pickupType == PickupType.Food)
            {
                player.AddHunger(Value);
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

}
