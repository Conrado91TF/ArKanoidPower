using System.Collections;
using UnityEngine;

public class LoserTrigger : MonoBehaviour
{
    [SerializeField] private float restartDelay = 1.0f; // segundos antes de reiniciar cuando queden vidas
    private bool isRestarting = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;

        // Referencia al GameManager
        var gm = GameManager.Instance;
        if (gm == null)
        {
            Debug.LogWarning("[LoserTrigger] GameManager.Instance es null.");
        }
        else
        {
            // Restar vida y sumar puntos: UI se actualiza inmediatamente
            gm.PerderVida();
            gm.SumarPuntos(100);
        }

        // Destruir la bola
        Destroy(collision.gameObject);

        // Si quedan vidas, reiniciar tras un delay para que se vea la actualización UI.
        // Si no quedan vidas, GameManager debería haber mostrado el panel Game Over y bloqueado recargas.
        if (gm != null && gm.ObtenerVidas() > 0 && !isRestarting)
        {
            StartCoroutine(RestartAfterDelay());
        }
    }

    private IEnumerator RestartAfterDelay()
    {
        isRestarting = true;
        yield return new WaitForSeconds(restartDelay);

        if (GameManager.Instance != null)
        {
            // Forzar reinicio para que la escena vuelva a cargar pese al estado IsGameOver (no aplica si IsGameOver=false)
            GameManager.Instance.ReiniciarJuego();
        }
        else
        {
            // Fallback directo
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        isRestarting = false;
    }
}