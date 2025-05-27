using UnityEngine;

using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;
using Unity.VisualScripting;

public class UpgradeMenu : MonoBehaviour
{   
    public GameManager Manager;
    public GameObject pauseButton; //GameObject reference for the pause menu button

    [Header("Upgrade Menu Effect variables")]
    public Button[] upgradeButtons;
    public GameObject[] buttonObjects;

    public float[] buttonXLocations;
    public CanvasGroup upgradeMenuFade;
    public AnimationCurve fadeInCurve;
    public AnimationCurve fadeOutCurve;
    public AnimationCurve slideInCurve;
    public AnimationCurve slideOutCurve;
    public float slideDuration;
    float menuOpenTime;
    float menuCloseTime;
    bool menuStart = true;
    bool menuClose = true;

    [Header("Upgrade 1")]
    public TMP_Text upgrade1Name;
    public TMP_Text upgrade1Tier;
    public TMP_Text upgrade1Description;

    [Header("Upgrade 2")]
    public TMP_Text upgrade2Name;
    public TMP_Text upgrade2Tier;
    public TMP_Text upgrade2Description;

    [Header("Upgrade 3")]
    public TMP_Text upgrade3Name;
    public TMP_Text upgrade3Tier;
    public TMP_Text upgrade3Description;

    //A variety of lists to compile upgrades and attacks from throughout the program
    private List<AttackStats> playerAttacks;
    private List<AttackStats> upgradableAttacks = new List<AttackStats>();
    public List<Upgrade> lockedAttacks; //List of all attacks the player is yet to unlock
    private List<Upgrade> playerUpgrades = new List<Upgrade>();
    private List<Upgrade> upgradePool = new List<Upgrade>();

    private Upgrade[] upgrades;
    public Upgrade healthUpgrade; //Default upgrade if the player has unlocked almost all the upgrades
    [HideInInspector] public Upgrade chosenUpgrade;
    [HideInInspector] public bool upgradeSelected = false;

    public int[] newAttackLevels; //Array of levels where the player is guaranteed to be offered a new attack if there are any available

    //Generating upgrades when the menu is made active
    public void OnEnable()
    {
        menuStart = true;
        menuClose = false;
        menuOpenTime = 0.0f;
        menuCloseTime = 0.0f;

        Manager.upgradeMenuOpen = true;
        pauseButton.SetActive(false); //Hiding the pause menu button while the upgrade menu is open as it is non-functional

        playerAttacks = Manager.player.AttackStatsList;
        playerUpgrades = Manager.player.upgrades;
        upgradableAttacks.Clear();

        chosenUpgrade = null;
        upgradeSelected = false;
        upgrades = new Upgrade[3]; //Creating a new empty list of three upgrade choices for the player

        //Adding any upgrades the player hasn't maxed out to the upgradableAttacks list
        foreach (AttackStats attack in playerAttacks)
        {
            if (!attack.upgradeMaxed) { upgradableAttacks.Add(attack); }
        }
        
        GenerateUpgrades();
        PresentUpgrades();
    }

    void Update()
    {
        if (menuStart)
        {
            int step = 0;
            foreach (Button upgradeButton in upgradeButtons)
            {
                upgradeButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(buttonXLocations[step], slideInCurve.Evaluate(menuOpenTime));
                upgradeMenuFade.alpha = fadeInCurve.Evaluate(menuOpenTime);
                step++;
            }
            menuOpenTime += Time.deltaTime;

            if (menuOpenTime > slideDuration)
            {
                menuStart = false;
                Time.timeScale = 0.0f;
            }
        }

        else if (menuClose)
        {
            int step = 0;
            foreach (Button upgradeButton in upgradeButtons)
            {
                upgradeButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(buttonXLocations[step], slideOutCurve.Evaluate(menuCloseTime));
                upgradeMenuFade.alpha = fadeOutCurve.Evaluate(menuCloseTime);
                step++;
            }
            menuCloseTime += Time.deltaTime;

            if (menuCloseTime > slideDuration)
            {
                upgradeSelected = true;
                this.gameObject.SetActive(false);
            }
        }
    }

    //Generates three upgrade choices for the player
    public void GenerateUpgrades()
    {
        bool newAttackOption = false;

        //If the player is at a level where they are guaranteed the option of a new attack, giving them one
        foreach (int level in newAttackLevels)
        {
            if (level == Manager.player.level)
            {
                newAttackOption = true;
            }
        }
        if (newAttackOption && lockedAttacks.Count != 0)
        {
            int upgradeSpot = (Random.Range(0, lockedAttacks.Count)); //Generating a new int between zero and the amount of locked attacks
            upgrades[0] = lockedAttacks[upgradeSpot];
        }

        else
        {
            //Setting the first upgrade to be a weapon the player already has if possible
            if (upgradableAttacks.Count > 1)
            {
                int upgradeSpot = (Random.Range(0, upgradableAttacks.Count)); //Generating a new int in between zero and the amount of upgradable attacks
                upgrades[0] = upgradableAttacks[upgradeSpot].GetNextUpgrade(); //Setting the first upgrade to the upgrade that corresponds with upgradeSpot
                upgradableAttacks.RemoveAt(upgradeSpot); //Removing the first upgrade from the upgradableAttacks list so it doesn't accidentally get used again later
            }
            else if (upgradableAttacks.Count == 1)
            {
                upgrades[0] = upgradableAttacks[0].GetNextUpgrade();
                upgradableAttacks.RemoveAt(0);
            }
        }
        
        //Creating a pool of all the available upgrades
        upgradePool = new List<Upgrade>();
        if (playerUpgrades != null && playerUpgrades.Count > 0) { foreach (Upgrade upgrade in playerUpgrades) { upgradePool.Add(upgrade); } }
        if (upgradableAttacks != null && upgradableAttacks.Count > 0) { foreach (AttackStats attack in upgradableAttacks) { upgradePool.Add(attack.GetNextUpgrade()); } }


        //If the player doesn't have their max weapon amount and they're not currently at a level where they're guaranteed a new weapon, new weapons are added to the upgrade pool
        if (Manager.player.AttackStatsList.Count < Manager.player.maxWeapons && !newAttackOption)
        {
            //Add new weapons
            foreach (Upgrade newAttack in lockedAttacks) { upgradePool.Add(newAttack); }
        }

        //Adding 2 more upgrades from the available upgrade pool to the player's upgrade choices
        int i = 1;
        if (upgrades[0] == null) { i--; } //If there was no available weapon for the first upgrade, sets i to 0 to generate 3 new upgrades instead of 2

        //If there are less than 3 upgrades in the upgrade pool, adding basic health up upgrades to the pool instead until there are enough upgrades to choose from
        while (upgradePool.Count < 3 - i) { upgradePool.Add(healthUpgrade); }

        Upgrade nextUpgrade;
        while (i < 3)
        {
            int nextUpgradeSpot = Random.Range(0, upgradePool.Count); //Generates a random number between 0 and the size of the upgradePool list
            nextUpgrade = upgradePool[nextUpgradeSpot];
            upgradePool.RemoveAt(nextUpgradeSpot); //Removes the upgrade from the upgradePool so it doesn't get generated again
            upgrades[i] = nextUpgrade; //Sets the upgrade at i to the next generated upgrade
            i++;
        }
    }

    //Shows the player their possible upgrade choices
    public void PresentUpgrades()
    {
        //Hard-coded now cause I cbf to make a struct for this, TMP text can't be in an array for some reason (?)
        upgrade1Name.SetText(upgrades[0].upgradeValues.UpgradeName);
        upgrade1Tier.SetText(upgrades[0].upgradeValues.UpgradeTier);
        upgrade1Description.SetText(upgrades[0].upgradeValues.UpgradeText);

        upgrade2Name.SetText(upgrades[1].upgradeValues.UpgradeName);
        upgrade2Tier.SetText(upgrades[1].upgradeValues.UpgradeTier);
        upgrade2Description.SetText(upgrades[1].upgradeValues.UpgradeText);

        upgrade3Name.SetText(upgrades[2].upgradeValues.UpgradeName);
        upgrade3Tier.SetText(upgrades[2].upgradeValues.UpgradeTier);
        upgrade3Description.SetText(upgrades[2].upgradeValues.UpgradeText);
    }

    //Script for when the upgrade button is pressed, sets the chosen upgrade to the buttons corresponding upgrade
    public void UpgradeButtonPressed(int buttonInt)
    {
        //Sets the chosen upgrade to the player's current upgrade choice
        chosenUpgrade = upgrades[buttonInt];
        chosenUpgrade.ApplyUpgrade();

        //Logic to update attack/upgrade lists
        if (chosenUpgrade.upgradeType == Upgrade.UpgradeType.NewAttack)
        {
            lockedAttacks.Remove(chosenUpgrade);
        }
        else if (chosenUpgrade.upgradeType == Upgrade.UpgradeType.Player)
        {
            Manager.player.upgrades.Remove(chosenUpgrade);
        }

        Manager.upgradeMenuOpen = false;
        pauseButton.SetActive(true); //Makes the pause button visible again upon resuming the game
        Time.timeScale = 1.0f;
        menuClose = true;
    }
}