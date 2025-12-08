using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausa;
    public GameObject pauseButton;

    public void PauseGame()
    {
        Time.timeScale = 0;
        menuPausa.SetActive(true);
        pauseButton.SetActive(false);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        menuPausa.SetActive(false);
        pauseButton.SetActive(true);
    }
    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}

