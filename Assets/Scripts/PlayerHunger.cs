using UnityEngine;
using UnityEngine.UI;
public class PlayerHunger : MonoBehaviour
{

    public Player player;
    public Image hungerBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            player = FindAnyObjectByType<Player>();
        }

        hungerBar.fillAmount = player.hunger / player.maxHunger;
    }

    // Update is called once per frame
    void Update()
    {
        hungerBar.fillAmount = player.hunger / player.maxHunger;
    }
}
