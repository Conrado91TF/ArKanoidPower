using UnityEngine;

public class Bloques : MonoBehaviour
{
    [SerializeField]
    public int puntosBloque = 10;

    [SerializeField]
    public GameObject efectoDestruccion; // Partículas o efecto visual
    public AudioClip sonidoDestruccion; // Sonido al destruirse

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

