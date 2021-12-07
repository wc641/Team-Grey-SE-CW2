using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverWinScreen : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void Restart()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("UnitTestEnv");
    }

    public void GoToMainMenu()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("MapMenu");
    }
}
