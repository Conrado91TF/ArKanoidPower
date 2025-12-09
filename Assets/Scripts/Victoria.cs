using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Victoria : MonoBehaviour
{
   public TMP_Text textPuntos;
   public GameObject panelVictoria;
    
    public void MostrarVictoria()
    {
        panelVictoria.SetActive(true);
        textPuntos.text = (("Puntos Finales: ") + FindAnyObjectByType<GameManager>().puntosTexto).ToString();

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
