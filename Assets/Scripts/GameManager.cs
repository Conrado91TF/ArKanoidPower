using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    private int blocksLeft;
    public static GameManager Instance { get; private set; }

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
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blocksLeft = GameObject.FindGameObjectsWithTag("Blocks").Length;

        Debug.Log("Bloques encontrados al iniciar: " + blocksLeft);
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
    public void ReloadScene()
    {         
        // Asegura que el tiempo de juego esté normal al recargar la escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}
