using UnityEngine;

public class LoserTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        // Detecta colisiones con otros objetos 
        if (collision.gameObject.CompareTag("Ball")) 
        {
            
            
            GameManager.Instance.PerderVida();
            GameManager.Instance.SumarPuntos(100);
            GameManager.Instance.ReloadScene();
           
            Destroy(collision.gameObject);
        }
        
    }
}
