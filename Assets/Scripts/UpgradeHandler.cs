using UnityEngine;

using System.Collections.Generic;

public class UpgradeMenu : MonoBehaviour
{
    [HideInInspector] public GameManager Manager;

    private List<AttackStats> playerAttacks;
    private List<AttackStats> upgradableAttacks;
    private List<Upgrade> playerUpgrades;
    private List<Upgrade> upgradePool;

    public Attack tentacleWhip;
    public Attack eldritchLaser;
    public Attack miasma;

    private Upgrade[] upgrades;

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
    }

    //Generates three upgrade choices for the player
    void GenerateUpgrades()
    {
        //Creating a new empty list of three upgrade choices for the player
        upgrades = new Upgrade[3];

        //Setting the first upgrade to be one of the weapons they already have
        upgrades[0] = upgradableAttacks[(Random.Range(0, playerAttacks.Count))].GetNextUpgrade();

        //Creating a pool of all the available upgrades
        upgradePool = new List<Upgrade>();
        foreach (Upgrade upgrade in playerUpgrades) { upgradePool.Add(upgrade); }
        foreach (AttackStats attack in upgradableAttacks) { upgradePool.Add(attack.GetNextUpgrade()); }

        //If the player doesn't have their max weapon amount yet new weapons are added to the upgrade pool
        if (Manager.player.AttackStatsList.Count < Manager.player.maxWeapons)
        {
            //Add new weapons (too tired to add this tn)
        }

        /*
        switch (playerAttacks.Count)
        {
            case 1:
                upgrades[0] = playerAttacks[0].GetNextUpgrade();
                break;

            case 2:
                upgrades[0] = playerAttacks[Random.Range(0, 1)].GetNextUpgrade();
                break;

            case 3:
                bool secondUpgradeSelected = false;
                upgrades[0] = playerAttacks[Random.Range(0, 2)].GetNextUpgrade();
                upgrades[1] = playerAttacks[Random.Range(0, 2)].GetNextUpgrade();

                while (!secondUpgradeSelected)
                {
                    if (upgrades[1] == upgrades[0]) { upgrades[1] = playerAttacks[Random.Range(0, 2)].GetNextUpgrade(); }
                    else { secondUpgradeSelected = true; }
                }

                break;
        }
        */
    }
}