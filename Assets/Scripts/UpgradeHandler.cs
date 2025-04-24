using UnityEngine;

using System.Collections.Generic;
using UnityEngine.UI;
using NUnit.Framework;

public class UpgradeMenu : MonoBehaviour
{
    //UI Elements for the upgrade menu
    public Button[] upgradeButtons;

    [HideInInspector] public GameManager Manager;

    //A variety of lists to compile upgrades and attacks from throughout the program
    private List<AttackStats> playerAttacks;
    private List<AttackStats> upgradableAttacks;
    public List<Upgrade> lockedAttacks; //List of all attacks the player is yet to unlock
    private List<Upgrade> playerUpgrades;
    private List<Upgrade> upgradePool;

    public Attack tentacleWhip;
    public Attack eldritchLaser;
    public Attack miasma;

    private Upgrade[] upgrades;
    private Upgrade chosenUpgrade;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAttacks = Manager.player.AttackStatsList;
        playerUpgrades = Manager.player.upgrades;
    }

    //Generating upgrades when the menu is made active
    void OnActive()
    {
        //Adding any upgrades the player hasn't maxed out to the upgradableAttacks list
        foreach (AttackStats attack in playerAttacks)
        {
            if (!attack.upgradeMaxed) { upgradableAttacks.Add(attack); }
        }

        GenerateUpgrades();
        PresentUpgrades();
    }

    //Generates three upgrade choices for the player
    void GenerateUpgrades()
    {
        //Creating a new empty list of three upgrade choices for the player
        upgrades = new Upgrade[3];

        //Setting the first upgrade to be one of the weapons they already have
        //Setting the first upgrade to always be a weapon the player already has
        upgrades[0] = upgradableAttacks[(Random.Range(0, playerAttacks.Count))].GetNextUpgrade();

        //Creating a pool of all the available upgrades
        upgradePool = new List<Upgrade>();
        foreach (Upgrade upgrade in playerUpgrades) { upgradePool.Add(upgrade); }
        foreach (AttackStats attack in upgradableAttacks) { upgradePool.Add(attack.GetNextUpgrade()); }


        //If the player doesn't have their max weapon amount yet new weapons are added to the upgrade pool
        if (Manager.player.AttackStatsList.Count < Manager.player.maxWeapons)
        {
            //Add new weapons (too tired to add this tn)
            foreach (Upgrade newAttack in lockedAttacks) { upgradePool.Add(newAttack); }
        }

        int i = 1;
        Upgrade nextUpgrade = new Upgrade();
        bool upgradeIsDupe = false;

        //Adding 2 more upgrades from the available upgrade pool to the player's upgrade choices
        while (i < 3)
        {
            nextUpgrade = upgradePool[Random.Range(0, upgradePool.Count)];
            //Checking if an upgrade is already in the 
            foreach (Upgrade upgrade in upgrades)
            {
                if (nextUpgrade == upgrade) { upgradeIsDupe = true; break; }
            }

            if (!upgradeIsDupe)
            {
                upgrades[i] = nextUpgrade;
                i++;
            }
        }

    }

    //Shows the player their possible upgrade choices
    void PresentUpgrades()
    {
        //Stretch Goal to add animation for cards coming onto screen and flipping over when selected
        for (int i = 0; i < upgrades.Length; i++)
        {
            //Getting both text elements of the button and customising them with the upgrade name and description respectively
            Text[] texts = upgradeButtons[i].GetComponentsInChildren<Text>();
            texts[0].text = upgrades[i].upgradeValues.UpgradeName;
            texts[1].text = upgrades[i].upgradeValues.UpgradeText;
        }
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
    }
}