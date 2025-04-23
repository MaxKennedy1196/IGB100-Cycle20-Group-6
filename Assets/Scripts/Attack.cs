using Unity.VisualScripting;
using UnityEngine;

//Depracated script, has been superceded by AttackStats scriptable objects
public class Attack : MonoBehaviour
{
    [HideInInspector] public float timer;

    [Header("Attack Variables")]
    public float attackDuration;
    public float attackTime;
    public float attackRange;
    public float attackDamage;
    public GameObject attackEffect;
    public Projectile attackProjectile;
    public AttackType attackType;

    public enum AttackType
    {
        Projectile,
        Radial,
        Emission
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        timer = Time.time + attackTime;
    }

    public void Use(float damageMult)
    {
        switch (attackType)
        {
            case AttackType.Projectile:
                TargetedAttack();
                break;

            case AttackType.Radial:
                RadialAttack();
                break;

            case AttackType.Emission:
                EmissionAttack();
                break;
        }
    }

    public void TargetedAttack()
    {
        //Fetching a list of every enemy in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Vector2 targetLocation = new Vector2(0,0);

        //Finding an enemy within attack range
        for (int i = 0; i < enemies.Length; i++)
        {
            if (Vector2.Distance(enemies[i].transform.position, transform.position) <= attackRange)
            {
                targetLocation = enemies[i].transform.position;
                break;
            }
        }
        if (targetLocation == null) { return; }

        Instantiate(attackEffect, transform.position, transform.rotation);

        Projectile projectile = Instantiate(attackProjectile, transform.position, transform.rotation);
        //projectile.targetLocation = targetLocation;
    }

    public void RadialAttack()
    {

    }

    public void EmissionAttack() 
    {
        
    }
}