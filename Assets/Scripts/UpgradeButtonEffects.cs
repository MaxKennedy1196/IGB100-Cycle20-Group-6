using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class UpgradeButtonEffects : MonoBehaviour
{
    public GameObject[] hideObjects;

    public RectTransform buttonLocation;
    public float xLocation;

    public CanvasGroup fade;
    public AnimationCurve fadeCurve;

    public AnimationCurve slideInCurve;
    public AnimationCurve hoverSizeCurve;

    public float slideInDuration;
    float slideTime = 0.0f;

    public float hoverDuration;
    float hoverTime = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SlideIn();
        //StartCoroutine(SlideIn());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(HoverOn());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(HoverOff());
    }

    public void SlideIn()
    {
        float slideTime = 0.0f;

        while (slideTime < slideInDuration)
        {
            buttonLocation.anchoredPosition = new Vector2(xLocation, slideInCurve.Evaluate(slideTime));
            fade.alpha = fadeCurve.Evaluate(slideTime);
            slideTime += Time.unscaledDeltaTime;
        }
    }

    /*
    public IEnumerator SlideIn()
    {
        float slideTime = 0.0f;

        while (slideTime < slideInDuration)
        {
            buttonLocation.anchoredPosition = new Vector2(xLocation, slideInCurve.Evaluate(slideTime));
            fade.alpha = fadeCurve.Evaluate(slideTime);
            slideTime += Time.unscaledDeltaTime;
        }
        yield return null;
    }
    */

    public IEnumerator HoverOn()
    {
        float hoverTime = 0.0f;

        while (hoverTime < hoverDuration)
        {
            buttonLocation.localScale = new Vector3(hoverSizeCurve.Evaluate(hoverTime), hoverSizeCurve.Evaluate(hoverTime), hoverSizeCurve.Evaluate(hoverTime));
            hoverTime += 0.02f;
        }
        yield return null;
    }

    public IEnumerator HoverOff()
    {
        float hoverTime = 0.0f;

        while (hoverTime < hoverDuration)
        {
            buttonLocation.localScale = new Vector3(1-hoverSizeCurve.Evaluate(hoverTime), 1-hoverSizeCurve.Evaluate(hoverTime), 1-hoverSizeCurve.Evaluate(hoverTime));
            hoverTime += Time.deltaTime;
        }
        yield return null;
    }
}