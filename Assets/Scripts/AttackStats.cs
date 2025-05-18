using Unity.VisualScripting;
//using UnityEditor.Rendering.Universal;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "AttackStats")]
public class AttackStats : ScriptableObject
{
    [Header("Initial Attack Variables")]
    public      float baseCooldown;
    public      float baseLifetime;//how long the attack remains active for
    public      float baseMinDamage;//min damage attack will deal to enemies
    public      float baseMaxDamage;//max damage attack will deal to enemies
    public      float baseSpeed;//speed of projectile
    public      float baseArea;//what distance the projectile must be from an enemy before it can damage it
    public      int   basePassthrough;// how many enemies this projectile can pass through before being deleted
    public      int   baseCritChance; //The base crit chance of this attack

    [Header("Attack Variables")]
    public float attackTimer;//how much longer before this attack is triggered again
    public float attackCooldown;//how long the attack will take to recharge after being modified by upgrades
    public float attackLifetime;//how long the attack remains active for
    public float attackMinDamage;//min damage attack will deal to enemies
    public float attackMaxDamage;//max damage attack will deal to enemies
    public float attackSpeed;//speed of projectile
    public float attackArea;//what distance the projectile must be from an enemy before it can damage it
    public int currentCritChance; //The current crit chance of this attack
    
    public int passthrough;// how many enemies this projectile can pass through before being deleted
    //[HideInInspector] public int projectileCount; //How many projectiles the attack fires; no logic for this implemented yet, may be removed
    public GameObject attackEffect;//effect to be played when attack is triggered
    public GameObject attackProjectile;//projectile to be fired when attack is triggered
    public TargetingType targetingType;// What type of targetting does this attack use
    public bool DPS;

    public bool returnOnDeath;

    [Header("Upgrades")]
    public Upgrade[] upgrades;

    [HideInInspector] public int upgradeTier = 0;
    [HideInInspector] public bool upgradeMaxed = false;

    public enum TargetingType
    {
        Closest,
        Random,
        Player,
        Mouse_Pointer
    }

    public void ResetAttack()
    {
        attackCooldown = baseCooldown;
        attackLifetime = baseLifetime;
        attackMinDamage = baseMinDamage;
        attackMaxDamage = baseMaxDamage;
        attackSpeed = baseSpeed;
        attackArea = baseArea;
        passthrough = basePassthrough;
        currentCritChance = baseCritChance;
    }

    public void DecreaseTimer()//decreases the attack timer over time
    {
        attackTimer -= Time.deltaTime;
    }

    public void ResetTimer()//reset attack timer to maximum cooldown
    {
        attackTimer = attackCooldown;
    }

    public void InitTimer()//start game
    {
        attackTimer = 0f;
    }

    //Function that creates a reference to the next upgrade for the UpgradeHandler
    public Upgrade GetNextUpgrade()
    {
        if (upgradeTier+1 == upgrades.Length) { upgradeMaxed = true; }
        return upgrades[upgradeTier];
    }
}