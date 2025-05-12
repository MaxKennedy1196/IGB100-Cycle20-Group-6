using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

//[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class Upgrade : MonoBehaviour
{
    public Player player;

    public enum UpgradeType
    {
        Player,
        Attack,
        NewAttack, 
        HealthUp
    }

    public UpgradeType upgradeType;

    [Serializable]
    public struct UpgradeTierValues
    {
        //General upgrade variables
        public string UpgradeName;
        public string UpgradeTier; //Text that displays the upgrade's tier
        public string UpgradeText; //Text that explains to the user what the upgrade does (+2 damage etc.)

        //Player upgrade variables
        public float MoveSpeedChange;
        public float MaxHealthChange;
        public float MaxHungerChange;
        public float XPGainChange;

        //Attack upgrade variables
        public Projectile attackProjectile;
        public AttackStats attackStats;

        public float AttackRateChangeAmount;
        public float DamageChangeAmount;
        public float RangeChangeAmount;
        public float AttackLifetimeChangeAmount;
        public int AttackPassthroughChangeAmount;
        public int ProjectileCountChangeAmount;

        //New attack variables
        public AttackStats NewAttackStats;

        //Health up variables
        public int HealthIncrease;
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
                player.health += upgradeValues.MaxHealthChange;
                player.maxHunger += upgradeValues.MaxHungerChange;
                player.hunger += upgradeValues.MaxHungerChange;
                player.experienceMult += upgradeValues.XPGainChange;
                break;

            //Upgrading attack values
            case UpgradeType.Attack:
                upgradeValues.attackStats.attackCooldown -= upgradeValues.AttackRateChangeAmount;
                upgradeValues.attackStats.attackMinDamage += upgradeValues.DamageChangeAmount;
                upgradeValues.attackStats.attackMaxDamage += upgradeValues.DamageChangeAmount;
                upgradeValues.attackStats.attackArea += upgradeValues.RangeChangeAmount; //Only used for miasma
                upgradeValues.attackStats.attackLifetime += upgradeValues.AttackLifetimeChangeAmount; //Increases projectile lifetime
                upgradeValues.attackStats.passthrough += upgradeValues.AttackPassthroughChangeAmount; //Increases projectile passthrough
                upgradeValues.attackStats.upgradeTier++; //Upgrades the attack's tier so the next available upgrade is accessed when necessary
                break;

            //Adding the new attack to the player's attacks
            case UpgradeType.NewAttack:
                player.AttackStatsList.Add(upgradeValues.NewAttackStats);
                break;
        }
    }
}