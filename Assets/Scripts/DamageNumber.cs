using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    public RectTransform container;
    public TMP_Text damageNum;
    public Vector3 offset;
    private Enemy enemy;
    private Transform trackedTransform;
    public float damageAmount;

    // Start is called before the first frame update
    public void OnEnable()
    {
        enemy = GetComponentInParent<Enemy>();
        trackedTransform = transform.parent;
    }

    void Update()
    {
        //Move the health bar
        Vector3 world = trackedTransform.position + offset;
        container.anchoredPosition = Camera.main.WorldToScreenPoint(world);
    }

    void ShowDamage()
    {
        damageNum.SetText(damageAmount.ToString());
    }
}