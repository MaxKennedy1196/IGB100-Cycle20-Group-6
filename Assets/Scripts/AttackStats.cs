using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "AttackStats")]
public class AttackStats : ScriptableObject
{
    [Header("Attack Variables")]
    public float attackCooldown;
    public float attackDuration; // How long the attack will be on screen for
    public float attackDamage;
    public float attackArea;
    public GameObject attackEffect;
    public GameObject projectile;
    public AttackType attackType;

    public enum AttackType
    {
        Projectile,
        Radial,
        Emission
    }
}
