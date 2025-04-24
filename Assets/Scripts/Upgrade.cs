using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class Upgrade : ScriptableObject
{
    public Player player;

    public enum UpgradeType
    {
        Player,
        Attack, 
        NewAttack
    }

    public UpgradeType upgradeType;

    [Serializable]
    public struct UpgradeTierValues
    {
        //General upgrade variables
        public string UpgradeName;
        public string UpgradeText; //Text that explains to the user what the upgrade does (+2 damage etc.)

        //Player upgrade variables
        public float MoveSpeedChange;
        public float MaxHealthChange;
        public float MaxHungerChange;
        public float XPGainChange;

        //Attack upgrade variables
        public AttackStats attack;
        public float AttackRateChangeAmount;
        public float DamageChangeAmount;
        public float RangeChangeAmount;
        public int ProjectileCountChangeAmount;

        //New attack variables
        public AttackStats NewAttack;
    }

    [SerializeField] public UpgradeTierValues upgradeValues;

    //Applies an upgrade according to the chosen type of the upgrade
    public void ApplyUpgrade()
    {
        player = GameManager.instance.player;

        switch (upgradeType)
        {
            //Upgrading player values
            case UpgradeType.Player:
                player.moveSpeed += upgradeValues.MoveSpeedChange;
                player.maxHealth += upgradeValues.MaxHealthChange;
                player.maxHunger += upgradeValues.MaxHungerChange;
                break;

            //Upgrading attack values
            case UpgradeType.Attack:
                upgradeValues.attack.attackTimer -= upgradeValues.AttackRateChangeAmount;
                upgradeValues.attack.attackMinDamage += upgradeValues.DamageChangeAmount;
                upgradeValues.attack.attackMaxDamage += upgradeValues.DamageChangeAmount;
                upgradeValues.attack.upgradeTier++;
                break;

            //Adding the new attack to the player's attacks
            case UpgradeType.NewAttack:
                player.AttackStatsList.Add(upgradeValues.NewAttack);
                break;

        }
    }
}