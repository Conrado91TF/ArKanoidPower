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
    }

    public void BlockDestroyed()
    {
        blocksLeft--;
        if (blocksLeft <= 0)
        {
            LoadNextlevel();
        }
    }
    private void LoadNextlevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ReloadScene()
    {         
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}
