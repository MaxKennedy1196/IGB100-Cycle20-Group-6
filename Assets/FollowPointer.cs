using UnityEngine;

public class FollowPointer : MonoBehaviour
{
    private Vector3 mousePosition;
	float moveSpeed = 999999f;


	// Update is called once per frame
	void Update () 
    {    
		mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
	}
}
