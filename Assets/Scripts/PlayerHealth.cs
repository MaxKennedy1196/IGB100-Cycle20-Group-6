using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Player player;
    public Image healthBar;
    public Animator healthAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created  
    void Start()
    {
        if (player == null)
        {
            player = FindAnyObjectByType<Player>();
        }

        healthBar.fillAmount = player.health / player.maxHealth;
    }

    // Update is called once per frame  
    void Update()
    {
        healthBar.fillAmount = player.health / player.maxHealth;

        if (player.health <= 30)
        {
            healthAnimator.SetBool("LowHealth", true);
        }
        else
        {
            healthAnimator.SetBool("LowHealth", false);
        }
    }
}
