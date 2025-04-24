using UnityEngine;

public class IconManager : MonoBehaviour
{

    public GameObject icon1;
    public GameObject icon2;
    public GameObject icon3;
    public GameObject icon4;

    private Player player;
    private int lastLevel = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObjectOfType<Player>();
        UpdateIcon();
    }

    // Update is called once per frame
    void Update()
    {
        int currentLevel = Mathf.FloorToInt(player.level);
        if (currentLevel != lastLevel)
        {
            UpdateIcon();
            lastLevel = currentLevel;
        }
    }

    void UpdateIcon()
    {
        icon1.SetActive(false);
        icon2.SetActive(false);
        icon3.SetActive(false);
        icon4.SetActive(false);

        int level = Mathf.FloorToInt(player.level);
        if (level >= 1 && level <=4)
            icon1.SetActive(true);
        else if (level >= 5 && level <= 9)
            icon2.SetActive(true);
        else if (level >= 10 && level <= 14)
            icon3.SetActive(true);
        else if (level >= 15)
            icon4.SetActive(true);
    }
}
