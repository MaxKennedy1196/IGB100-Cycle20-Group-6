using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class UpgradeButtonEffects : MonoBehaviour
{
    public RectTransform buttonLocation;
    public float xLocation;

    public CanvasGroup fade;
    public AnimationCurve fadeInCurve;
    public AnimationCurve slideInCurve;

    public float slideInDuration;
    float menuOpenTime = 0.0f;

    bool menuStart = true;

    private void Awake()
    {
        menuOpenTime = 0.0f;
    }

    void Update()
    {
        if (menuStart)
        {
            buttonLocation.anchoredPosition = new Vector2(xLocation, slideInCurve.Evaluate(menuOpenTime));
            fade.alpha = fadeInCurve.Evaluate(menuOpenTime);
        }

        menuOpenTime += Time.deltaTime;

        if (menuOpenTime > slideInDuration)
        {
            menuStart = false;
            Time.timeScale = 0.0f;
        }
    }
}