using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int blocksLeft;
    public static GameManager Instance { get; private set; }

    [Header("Puntuación")]
    public TextMeshProUGUI puntosTexto;
    private int puntos = 0;

    [Header("Vidas")]
    public Image[] corazones; // Array de 3 imágenes de corazones
    private int vidas = 3;

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

        ActualizarPuntos();
        ActualizarCorazones();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

    // Método para perder una vida
    public void PerderVida()
    {
        if (vidas > 0)
        {
            vidas--;
            ActualizarCorazones();
            Debug.Log("Vidas restantes: " + vidas);

            if (vidas <= 0)
            {
                
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
    public void ReloadScene() // Método para recargar la escena actual
    {
        
       // Asegura que el tiempo de juego esté normal al recargar la escena
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);



    }
}
