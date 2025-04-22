using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "AttackStats")]
public class AttackStats : ScriptableObject
{
    [Header("Attack Variables")]
    public      float attackCooldown;//how long the attack will take to recharge
    public      float attackTimer;//how much longer before this attack is triggered again
    public      float attackLifetime;//how long the attack remains active for
    public      float attackDamage;//damage attack will deal to enemies
    public      float attackSpeed;//speed of projectile
    public      float attackArea;//what distance the projectile must be from an enemy before it can damage it
    public int passthrough;// how many enemies this projectile can pass through before being deleted
    public GameObject attackEffect;//effect to be played when attack is triggered
    public GameObject attackProjectile;//projectile to be fired when attack is triggered
    public AttackType attackType;// what type of attack is this? (Projectile, Radial, Emission)

    public enum AttackType
    {
        Projectile,
        Radial,
        Emission
    }

    public TargettingType targettingType;// What type of targetting does this attack use

    public enum TargettingType
    {
        Closest,
        Random,
        None
    }

    public void decreseTimer()//decreases the attack timer over time
    {
        attackTimer -= Time.deltaTime;
        
    }

    public void resetTimer()//reset attack timer to maximum cooldown
    {
        attackTimer = attackCooldown;
        
    }

    void Start()
    {
        resetTimer();
    }

}
