using UnityEngine;
using UnityEngine.UI;

public class PlayerExperience : MonoBehaviour
{

    public Player player;
    public Image experienceBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            player = FindAnyObjectByType<Player>();

            experienceBar.fillAmount = player.experience / player.maxExperience;
        }
    }

    // Update is called once per frame
    void Update()
    {
            experienceBar.fillAmount = player.experience / player.maxExperience;
    }
}
