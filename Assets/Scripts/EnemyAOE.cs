using System;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class EnemyAOE : MonoBehaviour
{
    [Header("AOE Values")]
    public float damageMin;
    public float damageMax;
    public float projectileLifetime;
    public float projectileArea; //Should be half the object's scale
    public float damageTick = 0.5f;

    public AnimationCurve opacityCurve;
    public SpriteRenderer spriteRenderer;
    private float opacityTime = 0;

    float distance;
    float damage;
    float DPSTimer = 0f;

    GameManager Manager;
    Player player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); //Find Player reference
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>(); //Find gamemanager reference
        Destroy(this, projectileLifetime);
    }

    public void Update()
    {
        spriteRenderer.color = new Color(0, 0.5f, 1, opacityCurve.Evaluate(opacityTime));
        opacityTime += Time.deltaTime;
        DPSTimer += Time.deltaTime;

        distance = Vector3.Distance(transform.position, player.transform.position); //Distance between the AOE and the player

        if (distance <= projectileArea) //If the player is within the AOE and at least 0.25 seconds has passed since the last tick of damage, deal damage and reset the damage tick timer
        {
            if (DPSTimer >= damageTick)
            {
                damage = UnityEngine.Random.Range(damageMin, damageMax);
                player.takeDamage(damage, true);
                DPSTimer = 0f;
            }
        }
    }
}