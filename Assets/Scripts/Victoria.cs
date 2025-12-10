using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Victoria : MonoBehaviour
{
    public TMP_Text textPuntos;
    public GameObject panelVictoria;

    [Header("Animación")]
    public float animDuration = 0.6f;
    public LeanTweenType animEase = LeanTweenType.easeOutBack;

    private void Awake()
    {
        if (panelVictoria != null)
        {
            panelVictoria.transform.localScale = Vector3.zero;
            panelVictoria.SetActive(false);
        }
        else
        {
            panelVictoria = GameObject.Find("PanelVictoria") ?? GameObject.Find("PanelVictory");
            if (panelVictoria != null)
            {
                panelVictoria.transform.localScale = Vector3.zero;
                panelVictoria.SetActive(false);
            }
        }
    }

    public void MostrarVictoria()
    {
        if (textPuntos != null)
        {
            if (GameManager.Instance != null)
                textPuntos.text = "Puntos Finales: " + GameManager.Instance.ObtenerPuntos().ToString();
            else
                textPuntos.text = "Puntos Finales: 0";
        }
        else
        {
            Debug.LogWarning("[Victoria] textPuntos no asignado en el Inspector.");
        }

        if (panelVictoria == null)
        {
            Debug.LogWarning("[Victoria] panelVictoria no asignado ni encontrado.");
            Time.timeScale = 0f;
            return;
        }

        panelVictoria.transform.localScale = Vector3.zero;
        panelVictoria.SetActive(true);

        LeanTween.scale(panelVictoria, Vector3.one, animDuration)
                 .setEase(animEase)
                 .setUseEstimatedTime(true);

        Time.timeScale = 0f;
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SalirAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}