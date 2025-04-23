//using NUnit.Framework;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

//The script for the basic tentacle attack, this is similar to the knife and whip attack from vampire survivors
public class TentacleAttack : MonoBehaviour
{/*
    //Basic variables that will be modified as the player gains upgrades
    [Header("Tentacle Variables")]
    public int tentacleRange = 3; //Need to tweak to figure out how far 3 actually goes
    public int tentacleDamage = 1; //Need to tweak so tentacles one shot basic enemies

    public LineRenderer tentacle;
    public bool tentacleMove = false;
    private GameObject[] tentacles;
    private GameObject[] enemies;
    private List<GameObject> targetEnemies;

    public void Start()
    {
        //base.Start();
        tentacle = GetComponent<LineRenderer>();
    }

    public void Update()
    {
        tentacle.SetPosition(0, transform.position);
        if (!tentacleMove) { tentacle.SetPosition(1, transform.position); }
    }

    public IEnumerator TentacleExtend(Vector2 targetPosition)
    {
        //Unfinished as of yet, Asher going to finish on 17/04
        tentacleMove = true;
        float midpointX = targetPosition.x - transform.position.x;
        Vector2 midpoint = new Vector2(Mathf.Lerp(0, Vector2.Distance(transform.position, targetPosition), 0.5f), 0);
        tentacle.SetPosition(1, targetPosition);
        yield return new WaitForSeconds(0.5f);
        tentacleMove = false;

        
        for (float i = 0.0f; i < 1; i += 0.1f)
        {
            tentacle.SetPosition(0, transform.position);

            //Mathf.Lerp(0, Vector2.Distance(transform.position, targetPosition), i);
            tentacle.SetPosition(1, targetPosition * i);
            yield return new WaitForSeconds(0.01f);
        }

        for (float i = 1.0f; i > 0; i -= 0.1f)
        {
            tentacle.SetPosition(0, transform.position);

            //Mathf.Lerp(0, Vector2.Distance(transform.position, targetPosition), i);
            tentacle.SetPosition(1, transform.position * i);
            yield return new WaitForSeconds(0.01f);
        }
        
    }

    public void Use(float damageMult)
    {   
        //Creating an array of the player's current tentacles
        tentacles = GameObject.FindGameObjectsWithTag("Tentacle");

        //Fetching a list of every enemy in the scene
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //Creating an empty array to add enemy targets to
        targetEnemies = new List<GameObject>();

        //Ending the attack cycle if no enemies are detected
        if (enemies.Length == 0) { return; }

        for (int i = 0; i < enemies.Length; i++)
        {
            if (Vector2.Distance(enemies[i].transform.position, transform.position) <= tentacleRange)
            {
                targetEnemies.Add(enemies[i]);
            }
        }

        //Ending the attack cycle if no enemies are within range
        if (targetEnemies.Count == 0) { return; }

        //Create a tentacle object for each tentacle the player has
        for (int i = 0; i <= tentacles.Length; i++)
        {
            Debug.Log($"There are {tentacles.Length} tentacles and {targetEnemies.Count} enemies to target");
            //Create tentacle effect from player position pointing in direction of enemy target
            
            tentacle.SetPosition(0, transform.position);
            tentacle.SetPosition(1, targetEnemies[i].transform.position);
            //StartCoroutine(TentacleExtend(tentacle, targetEnemies[i].transform.position));
        }
    }*/
}