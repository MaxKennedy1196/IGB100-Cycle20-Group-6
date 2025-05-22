using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [HideInInspector] public GameManager Manager;
    public GameObject pauseMenu;
    public GameObject pauseButton;

    public bool isPaused;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !Manager.upgradeMenuOpen)
        {
            togglePause();
        }
    }


    // Update is called once per frame
    public void togglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }

        //if (!Manager.upgradeMenuOpen) //Handling to disable the pause functionality when the upgrade menu is open
        //{

        //}
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
