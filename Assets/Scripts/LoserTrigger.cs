using UnityEngine;

public class LoserTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        // Detecta colisiones con otros objetos 
        if (collision.gameObject.CompareTag("Ball")) 
        {
            // Verifica si el objeto que colisionó tiene la etiqueta "Ball"
            GameManager.Instance.ReloadScene(); 
            // Llama al método ReloadScene del GameManager para reiniciar la escena
            
        }
    }
}
