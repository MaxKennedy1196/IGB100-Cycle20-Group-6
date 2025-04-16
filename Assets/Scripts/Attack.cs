using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [HideInInspector] public float timer;
    public float attackTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        timer = Time.time + attackTime;
    }

    public abstract void Use(float damageMult);
}