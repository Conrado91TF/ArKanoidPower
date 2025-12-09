using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GamerOver : MonoBehaviour
{
    public TMP_Text textPuntos;

    public GameObject panelGamerOver;

    public void MostrarGameOver()
    {
       panelGamerOver.SetActive(true);

       textPuntos.text = (("Puntos Finales: " )+ FindAnyObjectByType<GameManager>().puntosTexto).ToString();

        Time.timeScale = 0f;
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


