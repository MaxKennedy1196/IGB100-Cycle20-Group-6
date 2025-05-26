using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerHunger : MonoBehaviour
{
    public Player player;
    public Image hungerBar;
    public Animator hungerAnimator;

    public GameObject indicatorPrefab; // Prefab with Image component
    public Transform indicatorParent;  // Container (like a horizontal layout group)
    public float incrementHunger = 20f;

    public Sprite fullSprite;  // Assign in Inspector
    public Sprite emptySprite; // Assign in Inspector

    private List<Image> indicatorImages = new List<Image>();
    private int lastIndicatorCount = -1;

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
        BuildIndicators();
        UpdateIndicators();
    }

    void Update()
    {
        hungerBar.fillAmount = player.hunger / player.maxHunger;

        if (Mathf.FloorToInt(player.maxHunger / incrementHunger) != lastIndicatorCount)
        {
            BuildIndicators();
        }

        UpdateIndicators();

        /*
        if (player.hunger <= 20)
        {
            hungerAnimator.SetBool("Hungry", true);
        }
        else
        {
            hungerAnimator.SetBool("Hungry", false);
        }
        */
    }

    void BuildIndicators()
    {
        foreach (Transform child in indicatorParent)
        {
            Destroy(child.gameObject);
        }

        indicatorImages.Clear();

        int numIndicators = Mathf.FloorToInt(player.maxHunger / incrementHunger);
        lastIndicatorCount = numIndicators;

        for (int i = 0; i < numIndicators; i++)
        {
            GameObject indicator = Instantiate(indicatorPrefab, indicatorParent);
            Image img = indicator.GetComponent<Image>();
            indicatorImages.Add(img);
        }
    }

    private void UpdateIndicators()
    {
        float hungerLeft = player.hunger;

        for (int i = 0; i < indicatorImages.Count; i++)
        {
            bool isFull = hungerLeft >= incrementHunger;
            indicatorImages[i].sprite = isFull ? fullSprite : emptySprite;
            hungerLeft -= incrementHunger;
        }
    }
}
