using TMPro.Examples;
using UnityEngine;
using TMPro;

public class PopupAnimation : MonoBehaviour
{
    public AnimationCurve opacityCurve;
    public AnimationCurve scaleCurve;
    public AnimationCurve heightCurve;

    private TextMeshProUGUI tmp;
    private float time = 0;
    private Vector3 origin;
    [HideInInspector] public bool critNum;

    private void Awake()
    {
        tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        origin = transform.position;
    }

    private void Update()
    {
        if (critNum) //Damage num showing if the player has hit a critical attack
        {
            //Add slight screen shake (?)
            tmp.color = new Color(1, 1, 0, opacityCurve.Evaluate(time)); //Yellow text for a crit
            transform.localScale = 2.0f * (Vector3.one * scaleCurve.Evaluate(time)); //Double size damage number for crits
            transform.position = origin + new Vector3(0, 1 + heightCurve.Evaluate(time), 0);
            time += Time.deltaTime;
        }

        else
        {
            tmp.color = new Color(1, 1, 1, opacityCurve.Evaluate(time));
            transform.localScale = Vector3.one * scaleCurve.Evaluate(time);
            transform.position = origin + new Vector3(0, 1 + heightCurve.Evaluate(time), 0);
            time += Time.deltaTime;
        }
    }
}