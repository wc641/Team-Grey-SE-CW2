using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/**
* Class that controls the pause menu behaivour.
*
* Essentially activiates and deactives gameobjects depending on state 
*/
namespace VS.CW2RTS.UI
{
public class PauseMenu : MonoBehaviour
    {
        public GameObject pauseMenu;
        public GameObject pausedScreen;
        public GameObject controlsScreen;

        public static bool isPaused;

        // Start is called before the first frame update
        void Start()
        {
            pauseMenu.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
                if(isPaused)
                    ResumeGame();
                else
                    PauseGame();
        }

        public void PauseGame()
        {
            pauseMenu.SetActive(true);
            pausedScreen.SetActive(true);
            controlsScreen.SetActive(false);
            Time.timeScale = 0f;
            isPaused = true;
        }

        public void ResumeGame()
        {
            pauseMenu.SetActive(false);
            controlsScreen.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }

        public void MainMenuPressed()
        {
            SceneManager.LoadScene("MapMenu");
            Time.timeScale = 1f;
            isPaused = false;
        }

        public void ShowControls()
        {
            controlsScreen.SetActive(true);
            pausedScreen.SetActive(false);
        }

        public void BackPressed()
        {
            controlsScreen.SetActive(false);
            pausedScreen.SetActive(true);
        }
    }
}
