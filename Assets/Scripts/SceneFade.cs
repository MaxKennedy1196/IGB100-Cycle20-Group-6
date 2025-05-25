using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFade : MonoBehaviour
{
    public float fadeDuration;
    public AnimationCurve fadeCurve;
    public float endWait = 0.0f;
    public AudioSource music;

    [SerializeField] CanvasGroup fade;
    public void ActivateFade()
    {
        StartCoroutine(ScreenFade());
    }

    public IEnumerator ScreenFade()
    {
        float fadingTime = 0.0f;

        while (fadingTime < fadeDuration)
        {
            fade.alpha = fadeCurve.Evaluate(fadingTime);
            if (music != null) music.volume = 1-(fadeCurve.Evaluate(fadingTime)); //1 minus curve value as music needs to fade the opposite way of the black
            fadingTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(endWait);
    }
}