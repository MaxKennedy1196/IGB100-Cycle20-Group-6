using UnityEngine;

public class PickUp : MonoBehaviour
{
    public float experienceAmount = 10;
    public AudioClip pickUpSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.AddExperience(experienceAmount);
                AudioSource.PlayClipAtPoint(pickUpSound, transform.position);
                Destroy(this.gameObject);
            }
        }
    }
}
