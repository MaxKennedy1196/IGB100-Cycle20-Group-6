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
    private List<AttackStats> upgradableAttacks;
    public List<Upgrade> lockedAttacks; //List of all attacks the player is yet to unlock
    private List<Upgrade> playerUpgrades;
    private List<Upgrade> upgradePool;

    private Upgrade[] upgrades;
    [HideInInspector] public Upgrade chosenUpgrade;
    [HideInInspector] public bool upgradeSelected = false;

    public Upgrade laser;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAttacks = Manager.player.AttackStatsList;
        playerUpgrades = Manager.player.upgrades;
    }

    //Generating upgrades when the menu is made active
    public void OnEnable()
    {
        chosenUpgrade = null;
        upgradeSelected = false;
        upgrades = new Upgrade[3]; //Creating a new empty list of three upgrade choices for the player
        /*
        //Adding any upgrades the player hasn't maxed out to the upgradableAttacks list
        foreach (AttackStats attack in playerAttacks)
        {
            if (!attack.upgradeMaxed) { upgradableAttacks.Add(attack); }
        }
        */

        GenerateUpgrades();
        PresentUpgrades();
    }

    //Generates three upgrade choices for the player
    public void GenerateUpgrades()
    {
        Debug.Log("Generating Upgrades");

        //Setting the first upgrade to always be a weapon the player already has
        //upgrades[0] = upgradableAttacks[(Random.Range(0, playerAttacks.Count))].GetNextUpgrade();
        upgrades[0] = laser;
        upgrades[1] = laser;
        upgrades[2] = laser;

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
        int i = 1;
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
        upgrade1Name.SetText(upgrades[0].upgradeValues.UpgradeName);
        upgrade1Description.SetText(upgrades[0].upgradeValues.UpgradeText);

        //Stretch Goal to add animation for cards coming onto screen and flipping over when selected
        for (int i = 0; i < upgrades.Length; i++)
        {
            //Getting both text elements of the button and customising them with the upgrade name and description respectively
            
            //TextMeshPro[] texts = upgradeButtons[i].GetComponentsInChildren<TextMeshPro>();
            //texts[0].text = upgrades[i].upgradeValues.UpgradeName;
            //texts[1].text = upgrades[i].upgradeValues.UpgradeText;
            
            //upgradeNames[i].GetComponent<TextMeshPro>().text = upgrades[i].upgradeValues.UpgradeName;
            //upgradeNames[i].GetComponent<TextMeshPro>().text = laser.upgradeValues.UpgradeName;
            //upgradeDescriptions[i].text = upgrades[i].upgradeValues.UpgradeText;
        }
    }

    //Script for when the upgrade button is pressed, sets the chosen upgrade to the buttons corresponding upgrade
    public void UpgradeButtonPressed(int buttonInt)
    {
        upgradeSelected = true;
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