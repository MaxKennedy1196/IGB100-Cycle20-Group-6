using UnityEngine;

using System.Collections.Generic;

public class UpgradeMenu : MonoBehaviour
{
    [HideInInspector] public GameManager Manager;

    public List<AttackStats> playerAttacks;

    public Attack tentacleWhip;
    public Attack eldritchLaser;
    public Attack miasma;

    private Upgrade[] upgrades;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAttacks = Manager.player.AttackStatsList;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Generating upgrades when the menu is made active
    void OnActive()
    {
        GenerateUpgrades();
    }

    void GenerateUpgrades()
    {
        upgrades = new Upgrade[3];

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

    }
}