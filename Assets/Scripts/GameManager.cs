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
    public TextMeshProUGUI puntosTexto;
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
            // Si ya existe una instancia del GameManager, destruye esta nueva instancia
            Destroy(gameObject);
            return;

        }
        Instance = this;
        DontDestroyOnLoad(gameObject);


    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (puntosTexto == null)
        {
            puntosTexto = GameObject.Find("PuntosTexto").GetComponent<TextMeshProUGUI>(); // *Cambia "PuntosTexto" por el nombre real de tu GameObject*
        }

        // 2. Reasignar Corazones (esto es más complejo, pero si el array está vacío o nulo, busca)
        if (corazones == null || corazones.Length == 0)
        {
            // Un ejemplo simplificado sería:
            // Busca el objeto principal que contiene los corazones y obtén sus hijos con el componente Image.
            GameObject contenedorCorazones = GameObject.Find("ContenedorCorazones"); // *Cambia por el nombre real*
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

        // Disminuye el conteo de bloques restantes y recarga la escena si no quedan bloques
        // BLOCKSLEFT se inicializa en Start contando todos los bloques con la etiqueta "Blocks"
        // ReloadScene se llama para reiniciar el juego cuando todos los bloques han sido destruidos
        blocksLeft--;
        if (blocksLeft <= 0)
        {
            ReloadScene();
        }
    }
    public void SumarPuntos(int cantidad)
    {
        puntos += cantidad;
        ActualizarPuntos();
    }

    // Actualizar el texto de puntos
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
                FindAnyObjectByType<GamerOver>().MostrarGameOver();
                Debug.Log("Game Over! Puntuación final: " + puntos);
                // Aquí puedes llamar a tu método de Game Over existente
            }

        }
    }

    // Actualizar visualización de corazones
    void ActualizarCorazones()
    {
        for (int i = 0; i < corazones.Length; i++)
        {
            if (i < vidas)
            {
                corazones[i].enabled = true; // Mostrar corazón
            }
            else
            {
                corazones[i].enabled = false; // Ocultar corazón
            }
        }
    }

    // Obtener puntos actuales
    public int ObtenerPuntos()
    {
        return puntos;

    }

    // Obtener vidas actuales
    public int ObtenerVidas()
    {
        return vidas;
    }
    public void ReiniciarJuego()
    {
        // Método público para botones de reinicio
        Time.timeScale = 1f; // Asegurar que el tiempo vuelva a la normalidad
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void ReloadScene() // Método para recargar la escena actual
    {
        Time.timeScale = 1f;
        // Asegura que el tiempo de juego esté normal al recargar la escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

   
}



