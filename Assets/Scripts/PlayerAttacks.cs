using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public List<Attack> Attacks;
    public float damageMult = 1.0f;

    // Update is called once per frame
    void Update()
    {
        foreach (Attack attack in Attacks)
        { 
            if (Time.time > attack.timer)
            { 
                attack.Use(damageMult);
                attack.timer = Time.time + attack.attackTime;
            }
        }
    }
}
