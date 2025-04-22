using System;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [Serializable]
    struct UpgradeValues
    {
        public bool projectileIncrease = false;
        public bool attackRateIncrease = false;
        public bool damageIncrease = false;
        public bool healthIncrease = false;
        public bool hungerIncrease = false;
        public bool movementSpeedIncrease = false;
    }

    [SerializeField] UpgradeValues upgradeValues;

    void ApplyUpgrade()
    {
        foreach (bool upgrade in upgradeValues)
        {
            if (upgrade) {; }
        }

    }
}