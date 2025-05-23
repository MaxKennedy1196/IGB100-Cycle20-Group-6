using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageNumber : MonoBehaviour
{

    public static DamageNumber Instance;
    public GameObject popupPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void CreatePopUp(Vector3 position, string text, bool crit)
    {
        var popup = Instantiate(popupPrefab, position, Quaternion.identity);
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        temp.text = text;
        if (crit) { popup.GetComponent<PopupAnimation>().critNum = crit; }

        Destroy(popup, 1f);
    }
}