using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{

    public float health = 100f;
    public float maxHealth = 100f;
    public float hunger = 100f;
    public float maxHunger = 100f;


    float moveSpeed = 5f;
    Vector2 moveVector = new Vector2(0,0);
    float xInput;
    float yInput;

    public List<Attack> Attacks;
    public float damageMult = 1.0f;

    private bool isdecaying = false;

    private float hungerDecayRate = 1f;
    private float hungerDecayTimer = 0f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Movement();

        hungerDecay();

        foreach (Attack attack in Attacks)
        { 
            if (Time.time > attack.timer)
            { 
                attack.Use(damageMult);
                attack.timer = Time.time + attack.attackTime;
            }
        }
        
    }

    private void Movement()
    {
        yInput = 0;
        xInput = 0;

        //get input for movement
        if (Input.GetKey(KeyCode.W))
        {
            yInput = 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            yInput = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            xInput = 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            xInput = -1f;
        }

        moveVector = new Vector2(xInput, yInput);//put input in a 2D Vector
        moveVector = Vector3.Normalize(moveVector);// normalize 2D vector

        transform.Translate(moveVector * moveSpeed * Time.deltaTime);//move player
    }

    public void takeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void hungerDecay()
    {
        hunger -= hungerDecayRate * Time.deltaTime;

        if (hunger <= 0)
        {
            hunger = 0;
            takeDamage(5f * Time.deltaTime);
        }
    }
}
