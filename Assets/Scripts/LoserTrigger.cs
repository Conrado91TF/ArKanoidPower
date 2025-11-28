using UnityEngine;

public class LoserTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            
            GameManager.Instance.ReloadScene();
        }
    }
}
