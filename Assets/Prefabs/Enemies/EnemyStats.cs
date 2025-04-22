using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float health;//Health of the enemy
    public float damage;//How much damage you want it to deal to the player per second
    public float moveSpeed;//How fast you want the enemy to be
    public float attackRange;//How far away from the player must the enmy be in order to deal damage to the player
    public float hungerProvided;// hunger provided for when the enemy dies.
    //public float damageRate = 1.0f;
    //float damageTime;
}
