using UnityEngine;
using UnityEditor;

public class LayerRandomiser : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();// this is a temporary fix to our clipping issue
        spriteRenderer.sortingOrder = Random.Range(-25,25);
    }

}
