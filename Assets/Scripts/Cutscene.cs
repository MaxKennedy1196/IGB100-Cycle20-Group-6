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

    public TypewriterEffect typewriter;

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

        typewriter._textBox = cutsceneData[frameNum].cutsceneText;
        typewriter._textBox.SetText(cutsceneData[frameNum].lines[lineNum]);
        typewriter.NextText();
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

        if (lineNum >= cutsceneData[frameNum].lines.Length)
        {
            lineNum = 0;
            StartCoroutine(FrameFadeOut());
        }
        else
        {
            //cutsceneData[frameNum].cutsceneText.SetText(cutsceneData[frameNum].lines[lineNum]);
            typewriter._textBox.SetText(cutsceneData[frameNum].lines[lineNum]);
            //typewriter.PrepareForNewText();
            typewriter.NextText();
        }
    }

    public void SkipPressed() //End the cutscene if the user chooses to
    {
        menu.RunGame();
    }

    public IEnumerator FrameFadeIn()
    {
        frameNum++;
        //Fade from black and set the next cutscene frame to active if these is another frame
        cutsceneData[frameNum].cutsceneFrame.SetActive(true);
        typewriter._textBox = cutsceneData[frameNum].cutsceneText;
        typewriter._textBox.SetText(cutsceneData[frameNum].lines[lineNum]);
        typewriter.NextText();

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