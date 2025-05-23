using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    private AsyncOperation loadSceneOperation; //Asynchronous operation to preload the next scene.

    public SceneFade fadeOut;
    public float fadeDuration;
    public AnimationCurve startFadeCurve;
    public AnimationCurve endFadeCurve;
    public AudioSource menuMusic;

    public Cutscene startCutscene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        fadeOut.fadeDuration = fadeDuration;
        fadeOut.fadeCurve = startFadeCurve;
        fadeOut.ActivateFade();
    }
    
    public void PlayGame()
    {
        startCutscene.Display();
    }

    // Update is called once per frame
    public void QuitGame()
    {
        StartCoroutine(FadeAndQuit());
    }

    public void MainMenu()
    {
        StartCoroutine(LoadNewScene(0));
    }

    public void RunGame()
    {
        StartCoroutine(LoadNewScene(1));
    }

    public bool CutsceneComplete() { return startCutscene.cutsceneComplete; }

    public IEnumerator LoadNewScene(int sceneNum)
    {
        fadeOut.fadeDuration = fadeDuration;
        fadeOut.fadeCurve = endFadeCurve;
        fadeOut.music = menuMusic;
        fadeOut.ActivateFade(); //Activating the screen fade and waiting out it's duration then activating the game scene
        yield return new WaitForSeconds(fadeOut.fadeDuration + fadeOut.endWait);
        SceneManager.LoadScene(sceneNum);
    }

    private IEnumerator FadeAndQuit()
    {
        fadeOut.fadeDuration = fadeDuration;
        fadeOut.fadeCurve = endFadeCurve;
        fadeOut.music = menuMusic;
        fadeOut.ActivateFade();
        yield return new WaitForSeconds(fadeOut.fadeDuration + fadeOut.endWait);
        Application.Quit();
    }
}