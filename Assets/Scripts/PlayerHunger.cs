using UnityEngine;
using UnityEngine.UI;
public class PlayerHunger : MonoBehaviour
{

    public Player player;
    public Image hungerBar;
    public Animator hungerAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            player = FindAnyObjectByType<Player>();
        }

        if (hungerBar == null)
        {
            hungerAnimator = GetComponentInParent<Animator>();
        }

        hungerBar.fillAmount = player.hunger / player.maxHunger;
    }

    // Update is called once per frame
    void Update()
    {
        hungerBar.fillAmount = player.hunger / player.maxHunger;

        if (player.hunger <= 20)
        {
            hungerAnimator.SetBool("Hungry", true);
        }
        else
        {
            hungerAnimator.SetBool("Hungry", false);
        }
    }
}
