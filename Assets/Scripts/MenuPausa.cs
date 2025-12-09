using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausa;
    public GameObject pauseButton;
    [SerializeField]
    private Vector3 posicionOriginal; // Guarda la posición final del menú
    private Vector3 posicionFuera;    // Posición fuera de pantalla

    void Start()
    {
        // Guarda la posición original del menú
        posicionOriginal = menuPausa.transform.localPosition;

        // Calcula posición fuera de pantalla (a la izquierda)
        posicionFuera = new Vector3(posicionOriginal.x, Screen.height, posicionOriginal.z);

        // Coloca el menú fuera de pantalla al inicio
        menuPausa.transform.localPosition = posicionFuera;
        menuPausa.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        menuPausa.SetActive(true);
        pauseButton.SetActive(false);

        // Resetea la posición antes de animar
        menuPausa.transform.localPosition = posicionFuera;

        // Animación de entrada desde la izquierda
        LeanTween.moveLocal(menuPausa, posicionOriginal, 0.5f)
            .setEase(LeanTweenType.easeOutQuad)
            .setIgnoreTimeScale(true); // IMPORTANTE: funciona aunque Time.timeScale = 0
    }

    public void ResumeGame()
    {
        // Animación de salida hacia la izquierda
        LeanTween.moveLocal(menuPausa, posicionFuera, 0.5f)
            .setEase(LeanTweenType.easeInQuad)
            .setIgnoreTimeScale(true)
            .setOnComplete(() => {
                Time.timeScale = 1f;
                menuPausa.SetActive(false);
                pauseButton.SetActive(true);
            });
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}