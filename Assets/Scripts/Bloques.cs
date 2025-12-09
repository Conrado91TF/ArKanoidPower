using TMPro;
using UnityEngine;

public class Bloques : MonoBehaviour
{
    [SerializeField]
    public int puntosBloque = 10;

    [SerializeField]
    public float escalaInicio = 1f;
    public float escalaMaxima = 1.2f;
    public float duracionAnimacion = 0.5f;
    public LeanTweenType tipoEasing = LeanTweenType.easeInOutSine;

    [SerializeField]
    public GameObject efectoDestruccion; // Partículas o efecto visual
    public AudioClip sonidoDestruccion; // Sonido al destruirse

    [SerializeField]
    public bool animarAlDestruir = true;
    public float duracionDestruccion = 0.3f;

    private Vector3 escalaOriginal;
    
    
    void Start()
    {
        escalaOriginal = transform.localScale;

        // Delay aleatorio para cada bloque
        float delayAleatorio = Random.Range(0f, 1f);

        LeanTween.scale(gameObject, escalaOriginal * escalaMaxima, duracionAnimacion)
            .setEase(LeanTweenType.easeInOutSine)
            .setLoopPingPong()
            .setDelay(delayAleatorio);

             
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si la bola golpeó el bloque
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Suma puntos al GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.SumarPuntos(puntosBloque);

                
            }
            Destroy(gameObject);
            // Efecto visual (opcional)
            if (efectoDestruccion != null)
            {
                Instantiate(efectoDestruccion, transform.position, Quaternion.identity);
            }

            // Sonido (opcional)
            if (sonidoDestruccion != null)
            {
                AudioSource.PlayClipAtPoint(sonidoDestruccion, transform.position);
            }

            // Destruir el bloque
            Destroy(gameObject);
        }
    }
    
}

