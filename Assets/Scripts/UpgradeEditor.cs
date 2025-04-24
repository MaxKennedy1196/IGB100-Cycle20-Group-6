using UnityEditor;
using System.Collections;
using UnityEngine;
using Unity.VisualScripting;
using System;

[CustomEditor(typeof(Upgrade)), CanEditMultipleObjects]
public class UpgradeEditor : Editor
{
    public string[] playerUpgradeOptions = { "MovementSpeed", "MaxHealth", "MaxHunger" };
    public string[] attackUpgradeOptions = { "AttackRate", "Damage", "Range", "ProjectileCount" };

    private Upgrade upgrade;
    override public void OnInspectorGUI()
    {
        // Unity provides a target when overriding the inspector look, and it has always the same type we declare on CustmoEditor(typeof()) on top
        upgrade = (Upgrade)target;

        //Changing the selected enum in the upgrade object based on the player 
        upgrade.upgradeType = (Upgrade.UpgradeType)EditorGUILayout.EnumPopup("Upgrade Type", upgrade.upgradeType);

        using (var group = new EditorGUILayout.FadeGroupScope(1))
        {
            //Displaying the variables for a player upgrade
            if (upgrade.upgradeType == Upgrade.UpgradeType.Player)
            {
                EditorGUILayout.PrefixLabel("Upgrade Name");
                upgrade.upgradeValues.UpgradeName = EditorGUILayout.TextField(upgrade.upgradeValues.UpgradeName);
                EditorGUILayout.PrefixLabel("Upgrade Text");
                upgrade.upgradeValues.UpgradeText = EditorGUILayout.TextField(upgrade.upgradeValues.UpgradeText);
                EditorGUILayout.PrefixLabel("Movement Speed Increase");
                upgrade.upgradeValues.MoveSpeedChange = EditorGUILayout.FloatField(upgrade.upgradeValues.MoveSpeedChange);
                EditorGUILayout.PrefixLabel("Max Health Increase");
                upgrade.upgradeValues.MaxHealthChange = EditorGUILayout.FloatField(upgrade.upgradeValues.MaxHealthChange);
                EditorGUILayout.PrefixLabel("Max Hunger Increase");
                upgrade.upgradeValues.MaxHungerChange = EditorGUILayout.FloatField(upgrade.upgradeValues.MaxHungerChange);
                EditorGUILayout.PrefixLabel("XP Gain Increase");
                upgrade.upgradeValues.XPGainChange = EditorGUILayout.FloatField(upgrade.upgradeValues.XPGainChange);
            }

            //Displaying the variables for an attack upgrade
            else if (upgrade.upgradeType == Upgrade.UpgradeType.Attack)
            {
                EditorGUILayout.PrefixLabel("Attack Name");
                upgrade.upgradeValues.UpgradeName = EditorGUILayout.TextField(upgrade.upgradeValues.UpgradeName);
                EditorGUILayout.PrefixLabel("Upgrade Text");
                upgrade.upgradeValues.UpgradeText = EditorGUILayout.TextField(upgrade.upgradeValues.UpgradeText);
                EditorGUILayout.PrefixLabel("Attack");
                upgrade.upgradeValues.attack = (AttackStats)EditorGUILayout.ObjectField(upgrade.upgradeValues.attack, typeof(AttackStats), true);
                EditorGUILayout.PrefixLabel("Attack Rate Increase");
                upgrade.upgradeValues.AttackRateChangeAmount = EditorGUILayout.FloatField(upgrade.upgradeValues.AttackRateChangeAmount);
                EditorGUILayout.PrefixLabel("Damage Increase");
                upgrade.upgradeValues.DamageChangeAmount = EditorGUILayout.FloatField(upgrade.upgradeValues.DamageChangeAmount);

                //Code for if/when projectile count upgrades are added
                //EditorGUILayout.PrefixLabel("Projectile Count Increase");
                //upgrade.upgradeValues.ProjectileCountChangeAmount = EditorGUILayout.IntField(upgrade.upgradeValues.ProjectileCountChangeAmount);
            }

            //Displaying the variables for a new attack upgrade
            else if (upgrade.upgradeType == Upgrade.UpgradeType.NewAttack)
            {
                EditorGUILayout.PrefixLabel("New Attack Name");
                upgrade.upgradeValues.UpgradeName = EditorGUILayout.TextField(upgrade.upgradeValues.UpgradeName);
                EditorGUILayout.PrefixLabel("Upgrade Text");
                upgrade.upgradeValues.UpgradeText = EditorGUILayout.TextField(upgrade.upgradeValues.UpgradeText);
                EditorGUILayout.PrefixLabel("New Attack");
                upgrade.upgradeValues.NewAttack = (AttackStats)EditorGUILayout.ObjectField(upgrade.upgradeValues.NewAttack, typeof(AttackStats), true);

            }
        }
    }