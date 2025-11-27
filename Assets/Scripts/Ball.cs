using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Vector2 initialVelocity; // Velocidad inicial de la bola al comenzar el juego.

    private Rigidbody2D ballRb; // Referencia al componente Rigidbody2D de la bola.

    private bool isBallMoving; // Indica si la bola ya está en movimiento.

    
    void Start()
    {
       ballRb = GetComponent<Rigidbody2D>();
        // Inicialmente, la bola no se está moviendo
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isBallMoving)
        {
            // isBallMoving es falso, lo que significa que la bola no está en movimiento
            // 1. Al presionar la barra espaciadora, inicia el movimiento de la bola si no está en movimiento.
            // Desvincula la bola de su padre (la paleta) para que pueda moverse libremente
            transform.parent = null;
            ballRb.linearVelocity = initialVelocity;
            isBallMoving = true;

        }
    }
}
