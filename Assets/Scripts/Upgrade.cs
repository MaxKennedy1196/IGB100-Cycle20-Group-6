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
                //upgradeValues.attack.attackCooldown -= upgradeValues.AttackRateChangeAmount; //Need to figure out how to make sure this targets the right variable without messing with the attackStats cooldown
                upgradeValues.attackProjectile.damageMin += upgradeValues.DamageChangeAmount;
                upgradeValues.attackProjectile.damageMax += upgradeValues.DamageChangeAmount;
                upgradeValues.attackProjectile.projectileArea += upgradeValues.RangeChangeAmount; //Only use for miasma
                upgradeValues.attackProjectile.projectileLifetime += upgradeValues.AttackLifetimeChangeAmount; //Increases projectile lifetime, best to only use for Miasma
                upgradeValues.attackProjectile.enemiesPassedThrough += upgradeValues.AttackPassthroughChangeAmount; //Increases projectile passthrough
                upgradeValues.attackStats.upgradeTier++; //Upgrades the attack's tier so the next available upgrade is accessed when necessary
                break;

            //Adding the new attack to the player's attacks
            case UpgradeType.NewAttack:
                player.AttackStatsList.Add(upgradeValues.NewAttackStats);
                break;
        }
    }
}