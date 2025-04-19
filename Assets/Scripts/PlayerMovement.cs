using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    float moveSpeed = 5f;
    Vector2 moveVector = new Vector2(0,0);
    float xInput;
    float yInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    void Update()
    {
        yInput = 0;
        xInput = 0;

        if(Input.GetKey(KeyCode.W))
        {
            yInput = 1f;
        }
        if(Input.GetKey(KeyCode.S))
        {
            yInput = -1f;
        }
        if(Input.GetKey(KeyCode.D))
        {
            xInput = 1f;
        }
        if(Input.GetKey(KeyCode.A))
        {
            xInput = -1f;
        }

        //xInput = Input.GetAxis("Horizontal");
        //yInput = Input.GetAxis("Vertical");

        moveVector = new Vector2(xInput,yInput);
        moveVector = Vector3.Normalize(moveVector);

        transform.Translate(moveVector * moveSpeed * Time.deltaTime);
        
    }
}
