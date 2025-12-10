using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GamerOver : MonoBehaviour
{
    public TMP_Text textPuntos;
    public GameObject panelGamerOver;

    private void Awake()
    {
        // Intento dejar el panel oculto al iniciar
        if (panelGamerOver != null)
            panelGamerOver.SetActive(false);
        else
        {
            // búsqueda automática por nombres comunes (opcional)
            panelGamerOver = GameObject.Find("PanelGameOver") ?? GameObject.Find("PanelGamerOver");
            if (panelGamerOver != null) panelGamerOver.SetActive(false);
        }
    }

    public void MostrarGameOver()
    {
        // Mostrar panel (si existe) y actualizar texto de puntos de forma segura
        if (panelGamerOver != null)
            panelGamerOver.SetActive(true);
        else
            Debug.LogWarning("[GamerOver] panelGamerOver no asignado ni encontrado.");

        if (textPuntos == null)
        {
            Debug.LogWarning("[GamerOver] textPuntos no asignado en el Inspector.");
        }
        else if (GameManager.Instance != null)
        {
            textPuntos.text = "Puntos Finales: " + GameManager.Instance.ObtenerPuntos().ToString();
        }
        else
        {
            Debug.LogWarning("[GamerOver] GameManager.Instance es null; no se pueden obtener puntos.");
            textPuntos.text = "Puntos Finales: 0";
        }

        Time.timeScale = 0f; // Pausa el juego
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f; // Restaura la velocidad del juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SalirAlMenu()
    {
        Time.timeScale = 1f; // Restaura la velocidad del juego
        SceneManager.LoadScene("MainMenu");
    }
}