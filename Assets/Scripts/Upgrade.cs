using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [HideInInspector] public bool playerSelected = false;

    public enum UpgradeType
    {
        Player,
        Attack
    }

    public UpgradeType upgradeType;

    //Player upgrade variables
    public float MoveSpeedChange;
    public float MaxHealthChange;
    public float MaxHungerChange;

    //Attack upgrade variables
    public float AttackRateChangeAmount;
    public float DamageChangeAmount;
    public float RangeChangeAmount;
    public float ProjectileCountChangeAmount;

    /*
    //Applies the upgrade to the object in question
    void ApplyUpgrade(int upgradeTier)
    {
        //Loops through all the upgrade variables and adds their respective upgrade amounts
        for (int i = 0; i < upgradeValues[upgradeTier].upgradeTargets.Count; i++)
        {
            upgradeValues[upgradeTier].upgradeTargets[i] += upgradeValues[upgradeTier].upgradeAmounts[i];
        }

    }*/
}