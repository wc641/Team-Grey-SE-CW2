using UnityEngine;
using UnityEngine.SceneManagement;

namespace VS.CW2RTS.UI
{
    public class LevelEndScreen : MonoBehaviour
    {
        public GameObject[] endScreens;
        private int currentScene;
        private int nextScene = 1;
        private int screenToShow;

        public void SetScreenToShow(bool hasWon)
        {
            screenToShow = hasWon ? 0 : 1;
            Setup(hasWon);
        }

        public void Setup(bool hasWon)
        {
            endScreens[screenToShow].SetActive(true);
            currentScene = SceneManager.GetActiveScene().buildIndex;

            if (hasWon)
            {
                if (currentScene != 7)
                {
                    nextScene = currentScene + 1;
                    if (nextScene > PlayerPrefs.GetInt("levelReached"))
                    {
                        PlayerPrefs.SetInt("levelReached", nextScene);
                    }
                }
            }
        }

        public void GoToNextLevel()
        {
            endScreens[screenToShow].SetActive(false);
            SceneManager.LoadScene(nextScene);
        }

        public void Restart()
        {
            endScreens[screenToShow].SetActive(false);
            SceneManager.LoadScene(currentScene);
        }

        public void GoToMainMenu()
        {
            endScreens[screenToShow].SetActive(false);
            SceneManager.LoadScene("MapMenu");
        }
    }
}