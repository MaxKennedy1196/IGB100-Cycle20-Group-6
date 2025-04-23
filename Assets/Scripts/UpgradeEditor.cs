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
                EditorGUILayout.PrefixLabel("Movement Speed Increase");
                upgrade.MoveSpeedChange = EditorGUILayout.FloatField(upgrade.MoveSpeedChange);
                EditorGUILayout.PrefixLabel("Max Health Increase");
                upgrade.MaxHealthChange = EditorGUILayout.FloatField(upgrade.MaxHealthChange);
                EditorGUILayout.PrefixLabel("Max Hunger Increase");
                upgrade.MaxHungerChange = EditorGUILayout.FloatField(upgrade.MaxHungerChange);
            }

            else
            {
                EditorGUILayout.PrefixLabel("Attack Rate Increase");
                upgrade.AttackRateChangeAmount = EditorGUILayout.FloatField(upgrade.AttackRateChangeAmount);
                EditorGUILayout.PrefixLabel("Damage Increase");
                upgrade.DamageChangeAmount = EditorGUILayout.FloatField(upgrade.DamageChangeAmount);
                EditorGUILayout.PrefixLabel("Range Increase");
                upgrade.RangeChangeAmount = EditorGUILayout.FloatField(upgrade.RangeChangeAmount);
                EditorGUILayout.PrefixLabel("Projectile Count Increase");
                upgrade.ProjectileCountChangeAmount = EditorGUILayout.FloatField(upgrade.ProjectileCountChangeAmount);
            }
        }
    }
}