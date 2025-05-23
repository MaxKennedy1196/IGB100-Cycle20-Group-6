using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    private AsyncOperation loadSceneOperation; //Asynchronous operation to preload the next scene.

    public SceneFade fadeOut;
    public AnimationCurve startFadeCurve;
    public AnimationCurve endFadeCurve;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        fadeOut.fadeCurve = startFadeCurve;
        fadeOut.ActivateFade();
        //loadSceneOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        //loadSceneOperation.allowSceneActivation = false;
    }
    
    public void PlayGame()
    {
        StartCoroutine(LoadNewScene(1));
        //StartCoroutine(LoadGame());
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

    public void Restart()
    {
        StartCoroutine(LoadNewScene(1));
    }

    public IEnumerator LoadGame()
    {
        fadeOut.fadeCurve = endFadeCurve;
        fadeOut.ActivateFade(); //Activating the screen fade and waiting out it's duration then activating the game scene
        yield return new WaitForSeconds(fadeOut.fadeDuration + fadeOut.endWait);
        //loadSceneOperation.allowSceneActivation = true;
    }

    private IEnumerator LoadNewScene(int sceneNum)
    {
        fadeOut.fadeCurve = endFadeCurve;
        fadeOut.ActivateFade(); //Activating the screen fade and waiting out it's duration then activating the game scene
        yield return new WaitForSeconds(fadeOut.fadeDuration + fadeOut.endWait);
        SceneManager.LoadScene(sceneNum);
    }

    private IEnumerator FadeAndQuit()
    {
        fadeOut.fadeCurve = endFadeCurve;
        fadeOut.ActivateFade();
        yield return new WaitForSeconds(fadeOut.fadeDuration + fadeOut.endWait);
        Application.Quit();
    }
}