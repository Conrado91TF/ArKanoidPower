using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GamerOver : MonoBehaviour
{
    public TMP_Text textPuntos;

    public GameObject panelGameOver;

    public void MostrarGameover()
    {
        panelGameOver.SetActive(true);
       
        
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

