using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int blocksLeft;
    public static GameManager Instance { get; private set; }

    [SerializeField]
    public TextMeshProUGUI puntosTexto; //Sistema de puntos
    private int puntos = 0;

    [SerializeField]
    public Image[] corazones; // Array de 3 imágenes de corazones
    private int vidas = 3;

    [SerializeField]
    private int bloquesRestantes = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (puntosTexto == null)
        {
            var go = GameObject.Find("PuntosTexto");
            if (go != null) puntosTexto = go.GetComponent<TextMeshProUGUI>();
        }

        if (corazones == null || corazones.Length == 0)
        {
            GameObject contenedorCorazones = GameObject.Find("ContenedorCorazones");
            if (contenedorCorazones != null)
            {
                corazones = contenedorCorazones.GetComponentsInChildren<Image>();
            }
        }

        blocksLeft = GameObject.FindGameObjectsWithTag("Blocks").Length;
        Debug.Log("Bloques encontrados al iniciar: " + blocksLeft);

        ActualizarPuntos();
        ActualizarCorazones();
    }

    public void BlockDestroyed()
    {
        blocksLeft--;
        Debug.Log("Bloques restantes: " + blocksLeft);

        if (blocksLeft <= 0)
        {
            // Intentar mostrar panel de victoria si existe en la escena
            var victoria = FindObjectOfType<Victoria>();
            if (victoria != null)
            {
                victoria.MostrarVictoria();
            }
            else
            {
                // Fallback: recargar o marcar victoria de otra forma
                Debug.Log("[GameManager] No se encontró componente Victoria en la escena. Recargando por fallback.");
                ReloadScene();
            }
        }
    }

    public void SumarPuntos(int cantidad)
    {
        //Suma
        puntos += cantidad;
        ActualizarPuntos();
    }

    void ActualizarPuntos()
    {
        if (puntosTexto != null)
        {
            puntosTexto.text = "Puntos: " + puntos.ToString();
        }
    }

    public int ObtenerBloquesRestantes()
    {
        return bloquesRestantes;
    }

    public void PerderVida()
    {
        if (vidas > 0)
        {
            vidas--;
            ActualizarCorazones();
            Debug.Log("Vidas restantes: " + vidas);

            if (vidas <= 0)
            {
                var gamerOver = FindObjectOfType<GamerOver>();
                if (gamerOver != null)
                    gamerOver.MostrarGameOver();
                else
                {
                    Debug.Log("[GameManager] No se encontró GamerOver; recargando por fallback.");
                    ReloadScene();
                }
                Debug.Log("Game Over! Puntuación final: " + puntos);
            }
        }
    }

    void ActualizarCorazones()
    {
        if (corazones == null) return;
        for (int i = 0; i < corazones.Length; i++)
        {
            corazones[i].enabled = i < vidas;
        }
    }

    public int ObtenerPuntos()
    {
        return puntos;
    }

    public int ObtenerVidas()
    {
        return vidas;
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}