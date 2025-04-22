using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "AttackStats")]
public class AttackStats : ScriptableObject
{
    [Header("Attack Variables")]
    public      float attackCooldown;
    public      float attackTimer;
    public      float attackLifetime;
    public      float attackDamage;
    public      float attackSpeed;
    public      float attackArea;
    public GameObject attackEffect;
    public GameObject attackLaserProjectile;
    public AttackType attackType;

    public enum AttackType
    {
        Projectile,
        Radial,
        Emission
    }

    public void decreseTimer()
    {
        attackTimer -= Time.deltaTime;
        
    }

    public void resetTimer()
    {
        attackTimer = attackCooldown;
        
    }

}
