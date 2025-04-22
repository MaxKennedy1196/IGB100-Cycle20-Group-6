using UnityEditor;
using System.Collections;
using UnityEngine;

[CustomEditor(typeof(Upgrade))]
public class UpgradeEditor : Editor
{
    private Upgrade upgrade;
    public override void OnInspectorGUI()
    {
        // Unity provides a target when overriding the inspector look, and it has always the same type we declare on CustmoEditor(typeof()) on top
        upgrade = (Upgrade)target;

        //Changing the selected enum in the upgrade object based on the player 
        upgrade.upgradeType = (Upgrade.UpgradeType)EditorGUILayout.EnumPopup("Upgrade Type", upgrade.upgradeType);

        switch (upgrade.upgradeType)
        {

            //Accessing variables for a player upgrade
            case Upgrade.UpgradeType.Player:
                upgrade.upgradeOptions = upgrade.playerUpgradeOptions; //Need to fix type conversion
                break;


        }
    }
}