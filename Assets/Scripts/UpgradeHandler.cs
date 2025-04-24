using UnityEngine;

using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;
using Unity.VisualScripting;

public class UpgradeMenu : MonoBehaviour
{   
    public GameManager Manager;
    //UI Elements for the upgrade menu
    public Button[] upgradeButtons;
    public TMP_Text upgrade1Name;
    public TMP_Text upgrade1Description;
    public TMP_Text upgrade2Name;
    public TMP_Text upgrade2Description;
    public TMP_Text upgrade3Name;
    public TMP_Text upgrade3Description;


    //A variety of lists to compile upgrades and attacks from throughout the program
    private List<AttackStats> playerAttacks;
    private List<AttackStats> upgradableAttacks = new List<AttackStats>();
    public List<Upgrade> lockedAttacks; //List of all attacks the player is yet to unlock
    private List<Upgrade> playerUpgrades = new List<Upgrade>();
    private List<Upgrade> upgradePool = new List<Upgrade>();

    private Upgrade[] upgrades;
    [HideInInspector] public Upgrade chosenUpgrade;
    [HideInInspector] public bool upgradeSelected = false;

    public Upgrade laser2;
    public Upgrade laser3;

    //Generating upgrades when the menu is made active
    public void OnEnable()
    {
        playerAttacks = Manager.player.AttackStatsList;
        playerUpgrades = Manager.player.upgrades;

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

    //Generates three upgrade choices for the player
    public void GenerateUpgrades()
    {
        Debug.Log("Generating Upgrades");

        //Setting the first upgrade to always be a weapon the player already has
        if (upgradableAttacks.Count > 1) { upgrades[0] = upgradableAttacks[(Random.Range(0, upgradableAttacks.Count))].GetNextUpgrade(); }
        else if (upgradableAttacks.Count == 1) { upgrades[1] = upgradableAttacks[0].GetNextUpgrade(); }

        upgrades[0] = laser2;
        upgrades[2] = laser3;

        /*
        //Creating a pool of all the available upgrades
        upgradePool = new List<Upgrade>();
        if (playerUpgrades != null) { foreach (Upgrade upgrade in playerUpgrades) { upgradePool.Add(upgrade); } }
        if (upgradableAttacks != null) { foreach (AttackStats attack in upgradableAttacks) { upgradePool.Add(attack.GetNextUpgrade()); } }


        //If the player doesn't have their max weapon amount yet new weapons are added to the upgrade pool
        if (Manager.player.AttackStatsList.Count < Manager.player.maxWeapons)
        {
            //Add new weapons
            foreach (Upgrade newAttack in lockedAttacks) { upgradePool.Add(newAttack); }
        }

        //Adding 2 more upgrades from the available upgrade pool to the player's upgrade choices
        int i = upgrades.Length();
        Upgrade nextUpgrade = new Upgrade();
        bool upgradeIsDupe = false;
        while (i < 3)
        {
            nextUpgrade = upgradePool[Random.Range(0, upgradePool.Count)];
            //Checking if an upgrade is already in the upgrade pool
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
        */
    }

    //Shows the player their possible upgrade choices
    public void PresentUpgrades()
    {
        //Hard-coded now cause I cbf to make a struct for this, TMP text can't be in an array for some reason (?)
        upgrade1Name.SetText(upgrades[0].upgradeValues.UpgradeName);
        upgrade1Description.SetText(upgrades[0].upgradeValues.UpgradeText);
        upgrade2Name.SetText(upgrades[1].upgradeValues.UpgradeName);
        upgrade2Description.SetText(upgrades[1].upgradeValues.UpgradeText);
        upgrade3Name.SetText(upgrades[2].upgradeValues.UpgradeName);
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

        Time.timeScale = 1.0f;
        upgradeSelected = true;
        this.gameObject.SetActive(false);
    }
}