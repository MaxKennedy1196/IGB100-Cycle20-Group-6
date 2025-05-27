using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

public class Cutscene : MonoBehaviour
{
    [HideInInspector] public bool cutsceneComplete = false;
    CutsceneData currentFrame;
    int lineNum = 0;
    int frameNum = -1;

    public mainMenu menu;

    public Button continueButton;
    public Button skipButton;

    [Header("Fade Variables")]
    public SceneFade fade;
    public float fadeDuration;
    public AnimationCurve startCurve;
    public AnimationCurve endCurve;

    [Serializable]
    public struct CutsceneData
    {
        public GameObject cutsceneFrame;
        public TMP_Text cutsceneText;
        public String[] lines;
    }

    [SerializeField] public CutsceneData[] cutsceneData;

    public void Display()
    {
        continueButton.gameObject.SetActive(true);
        skipButton.gameObject.SetActive(true);
        fade.fadeDuration = fadeDuration;
        fade.music = null;
        StartCoroutine(FrameFadeIn());

        cutsceneData[frameNum].cutsceneText.SetText(cutsceneData[frameNum].lines[lineNum]);

    }

    void Update()
    {
        if (lineNum >= 5)
        {
            menu.RunGame();
        }
    }

    public void ContinuePressed() //Advances to the next line if possible, if there is no next line then go to the next cutscene frame
    {
        lineNum++;
        Debug.Log(lineNum.ToString());
        Debug.Log(cutsceneData[frameNum].lines.Length);

        if (lineNum >= cutsceneData[frameNum].lines.Length)
        {
            lineNum = 0;
            Debug.Log(frameNum.ToString());
            StartCoroutine(FrameFadeOut());
        }
        else
        {
            cutsceneData[frameNum].cutsceneText.SetText(cutsceneData[frameNum].lines[lineNum]);
        }
    }

    public void SkipPressed() //End the cutscene if the user chooses to
    {
        menu.RunGame();
    }

    public IEnumerator WriteTextLine(string line, TMP_Text message, float startDelay = 0f, float timePerCharacter = 0.02f, float characterFadeTime = 1f)
    {
        float fadeStart = Time.time;
        float fadeEnd = fadeStart + timePerCharacter * line.Length + characterFadeTime;

        while (Time.time < fadeEnd)
        {
            string fadedText = string.Empty;
            bool addedClear = false;

            for (int i = 0; i < line.Length; i++)
            {
                Color c = message.color;
                float timeSinceCharacterFadeIn = Time.time - (fadeStart + timePerCharacter * i);

                if (timeSinceCharacterFadeIn <= 0)
                {
                    if (!addedClear)
                    {
                        fadedText += $"<color=#{ColorUtility.ToHtmlStringRGBA(Color.clear)}>";
                        addedClear = true;
                    }
                }
                else if (timeSinceCharacterFadeIn < characterFadeTime)
                {
                    c.a = timeSinceCharacterFadeIn / characterFadeTime;
                    fadedText += $"<color=#{ColorUtility.ToHtmlStringRGBA(c)}>";
                }
                fadedText += line[i];
            }

            message.text = fadedText;
            yield return null;
        }

        message.SetText(line);
    }

    public IEnumerator FrameFadeIn()
    {
        frameNum++;
        //Fade from black and set the next cutscene frame to active if these is another frame
        cutsceneData[frameNum].cutsceneFrame.SetActive(true);
        cutsceneData[frameNum].cutsceneText.SetText(cutsceneData[frameNum].lines[lineNum]);
        fade.fadeCurve = startCurve;
        fade.ActivateFade();
        yield return new WaitForSeconds(fadeDuration);
    }

    public IEnumerator FrameFadeOut()
    {
        if (frameNum + 1 >= cutsceneData.Length) { menu.RunGame(); }
        else
        {
            fade.fadeCurve = endCurve;
            fade.ActivateFade();
            yield return new WaitForSeconds(fadeDuration);

            cutsceneData[frameNum].cutsceneFrame.SetActive(false);
            StartCoroutine(FrameFadeIn());
        }
    }
}