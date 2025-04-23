using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public enum UpgradeType
    {
        Player,
        Attack
    }

    public UpgradeType upgradeType;


    
    /*public enum PlayerUpgradeOptions
    {
        MovementSpeed,
        MaxHealth,
        MaxHunger
    }
    public PlayerUpgradeOptions playerUpgradeOptions;
    

    public enum AttackUpgradeOptions
    {
        AttackRate,
        Damage,
        Range,
        ProjectileCount
    }
    public AttackUpgradeOptions attackUpgradeOptions;
    

    //Custom struct that creates two corresponding lists of variables to be upgraded and amounts to upgrade them by
    

    public struct PlayerUpgradeValues
    {
        public PlayerUpgradeOptions upgradeOptions;
        public List<float> playerUpgradeAmounts;
    }

    public struct AttackUpgradeValues
    {
        public AttackUpgradeOptions upgradeOptions;
        public List<float> playerUpgradeAmounts;
    }
    */

    [Serializable]
    public struct UpgradeValues
    {
        //Make list of enums/one single enum of different possible upgrade targets for player and one for possible upgrade targets for attacks

        public enum UpgradeOptions { };
        public List<float> upgradeAmounts;
    }
    [SerializeField] public UpgradeValues[] upgradeValues;


    //Applies the upgrade to the object in question
    void ApplyUpgrade(int upgradeTier)
    {
        //Loops through all the upgrade variables and adds their respective upgrade amounts
        for (int i = 0; i < upgradeValues[upgradeTier].upgradeTargets.Count; i++)
        {
            upgradeValues[upgradeTier].upgradeTargets[i] += upgradeValues[upgradeTier].upgradeAmounts[i];
        }

    }
}