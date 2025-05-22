using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    private AsyncOperation loadSceneOperation; //Asynchronous operation to preload the next scene.

    [SerializeField] private CanvasGroup fadeOut; //CanvasGroup for fade effect.
    public AudioSource menuMusic;
    public AnimationCurve fade; //Curve that controls the fade out into the game

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        loadSceneOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        loadSceneOperation.allowSceneActivation = false;
    }
    
    public void PlayGame()
    {
        StartCoroutine(LoadGame());
    }

    // Update is called once per frame
    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    
    //Loads game with fade in
    private IEnumerator LoadGame()
    {
        float fadeTime = 0.0f;

        while (fadeTime < 3.0f)
        {
            fadeOut.alpha = 1-(fade.Evaluate(fadeTime));
            menuMusic.volume = fade.Evaluate(fadeTime);
            fadeTime += Time.deltaTime;
            yield return null;
        }

        fadeOut.alpha = 1.0f;
        yield return new WaitForSeconds(0.5f);
        loadSceneOperation.allowSceneActivation = true;
    }
}