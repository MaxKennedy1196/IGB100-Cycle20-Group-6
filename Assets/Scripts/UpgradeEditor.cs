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
            if (upgrade.upgradeType == Upgrade.UpgradeType.Player)
            {
                EditorGUILayout.PrefixLabel("Upgrade Name");
                upgrade.upgradeTiers.UpgradeName = EditorGUILayout.TextField(upgrade.upgradeTiers.UpgradeName);
                EditorGUILayout.PrefixLabel("Movement Speed Increase");
                upgrade.upgradeTiers.MoveSpeedChange = EditorGUILayout.FloatField(upgrade.upgradeTiers.MoveSpeedChange);
                EditorGUILayout.PrefixLabel("Max Health Increase");
                upgrade.upgradeTiers.MaxHealthChange = EditorGUILayout.FloatField(upgrade.upgradeTiers.MaxHealthChange);
                EditorGUILayout.PrefixLabel("Max Hunger Increase");
                upgrade.upgradeTiers.MaxHungerChange = EditorGUILayout.FloatField(upgrade.upgradeTiers.MaxHungerChange);
            }

            else if (upgrade.upgradeType == Upgrade.UpgradeType.Attack)
            {
                EditorGUILayout.PrefixLabel("Attack Name");
                upgrade.upgradeTiers.AttackName = EditorGUILayout.TextField(upgrade.upgradeTiers.AttackName);
                EditorGUILayout.PrefixLabel("Attack Rate Increase");
                upgrade.upgradeTiers.AttackRateChangeAmount = EditorGUILayout.FloatField(upgrade.upgradeTiers.AttackRateChangeAmount);
                EditorGUILayout.PrefixLabel("Damage Increase");
                upgrade.upgradeTiers.DamageChangeAmount = EditorGUILayout.FloatField(upgrade.upgradeTiers.DamageChangeAmount);
                EditorGUILayout.PrefixLabel("Projectile Count Increase");
                upgrade.upgradeTiers.ProjectileCountChangeAmount = EditorGUILayout.IntField(upgrade.upgradeTiers.ProjectileCountChangeAmount);
            }

            else if (upgrade.upgradeType == Upgrade.UpgradeType.NewAttack)
            {
                EditorGUILayout.PrefixLabel("New Attack Name");
                upgrade.upgradeTiers.NewAttackName = EditorGUILayout.TextField(upgrade.upgradeTiers.NewAttackName);
            }
        }
    }
}