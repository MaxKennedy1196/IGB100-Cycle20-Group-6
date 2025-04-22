using UnityEngine;

public class PickUp : MonoBehaviour
{
    public float experienceAmount = 10;
    public AudioClip pickUpSound;
    public Player player;
    public Transform playerTransform;
    float pickupDistance = 1f;
    float distance = 0f;
    

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
            player.AddExperience(experienceAmount);
            AudioSource.PlayClipAtPoint(pickUpSound, transform.position);
            Destroy(this.gameObject);
        }
    }

}
