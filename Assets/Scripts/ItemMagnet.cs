using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PickUp>(out PickUp pickup))
        {
            pickup.SetTarget(transform.parent.position);
        }
        
    }
}
