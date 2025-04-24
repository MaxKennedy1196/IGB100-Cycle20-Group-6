using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class Upgrade : ScriptableObject
{
    [HideInInspector] public bool playerSelected = false;

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
        //Player upgrade variables
        public string UpgradeName;
        public float MoveSpeedChange;
        public float MaxHealthChange;
        public float MaxHungerChange;

        //Attack upgrade variables
        public string AttackName;
        public float AttackRateChangeAmount;
        public float DamageChangeAmount;
        public float RangeChangeAmount;
        public int ProjectileCountChangeAmount;

        //New attack variables
        public string NewAttackName;
    }

    [SerializeField] public UpgradeTierValues upgradeTiers;

    //Applies an attack upgrade
    void ApplyAttackUpgrade(AttackStats attack)
    {
        attack.attackTimer -= upgradeTiers.AttackRateChangeAmount;
        //Not sure how we're doing damage increases but currently damage increase upgrades boost the min and max by the same amount
        attack.attackMinDamage += upgradeTiers.DamageChangeAmount;
        attack.attackMaxDamage += upgradeTiers.DamageChangeAmount;
        //Also not sure if projectile count upgrades are being added or not
        attack.projectileCount += upgradeTiers.ProjectileCountChangeAmount;
        attack.upgradeTier++;
    }

    //Applies a player upgrade
    void ApplyPlayerUpgrade()
    {
        Player player = GameManager.instance.player;
        player.moveSpeed += upgradeTiers.MoveSpeedChange;
        player.maxHealth += upgradeTiers.MaxHealthChange;
        player.maxHunger += upgradeTiers.MaxHungerChange;
    }
}