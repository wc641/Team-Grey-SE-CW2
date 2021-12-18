using UnityEngine;
using UnityEngine.SceneManagement;


namespace VS.CW2RTS.UI
{
    public class SceneSwitcher : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }

}
