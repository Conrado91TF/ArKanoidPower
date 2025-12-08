using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Vector2 initialVelocity; // Velocidad inicial de la bola al comenzar el juego.
    // Vector2 se usa porque el juego es en 2D.

    private Rigidbody2D ballRb; // Referencia al componente Rigidbody2D de la bola.
    // Rigidbody2D se usa para manejar la física en 2D.
    //ballRb se usa para acceder y modificar la velocidad de la bola.

    private bool isBallMoving = false; // Indica si la bola ya está en movimiento.
    // isBallMoving se usa para evitar que la bola se mueva más de una vez al presionar la barra espaciadora.

    [SerializeField]
    private float velocityMultiplaier; // Multiplicador de velocidad al destruir un ladrillo.
    public float playerOffsetY = 0.4f;

    

    
   
    void Start()
    {
        ballRb = GetComponent<Rigidbody2D>();
        // Inicialmente, la bola no se está moviendo
        // GetComponent<Rigidbody2D>() obtiene el componente Rigidbody2D adjunto al mismo GameObject que este script.
        
        


    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isBallMoving)
        {
            // linearVelocity es una propiedad de Rigidbody2D que establece la velocidad lineal del objeto.
            // transform.parent se refiere al padre del objeto actual (la bola).
            // isBallMoving es falso, lo que significa que la bola no está en movimiento
            // 1. Al presionar la barra espaciadora, inicia el movimiento de la bola si no está en movimiento.
            // Desvincula la bola de su padre (la paleta) para que pueda moverse libremente
            transform.parent = null;
            ballRb.linearVelocity = initialVelocity;
            isBallMoving = true;

        }
    
        if (Input.GetButtonDown("Jump"))
        {
            Tirar();
        }
    }   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Blocks"))
        {
            // Destroy se usa para destruir un objeto en Unity.
            // OnCollisionEnter2D se llama cuando la bola colisiona con otro objeto.
            // CompareTag("Block") verifica si el objeto con el que la bola ha colisionado tiene la etiqueta "Block".
            // collision.gameObject se refiere al objeto con el que la bola ha colisionado.
            // Destruye el ladrillo al colisionar con la bola
            Destroy(collision.gameObject);
            ballRb.linearVelocity *= velocityMultiplaier; // Aumenta la velocidad de la bola al destruir un ladrillo

            GameManager.Instance.BlockDestroyed();
        }
        CorregirAngulo();
    }
    
    void CorregirAngulo()
    {
        
        float anguloMinimo = 15f;
        Vector2 vel = ballRb.linearVelocity;

        float angulo = Mathf.Abs(Vector2.Angle(vel, Vector2.right));

        // Si está demasiado vertical (entre 75° y 105°)
        if (angulo > 90f - anguloMinimo && angulo < 90f + anguloMinimo)
        {
            float signoX = vel.x >= 0 ? 1f : -1f;
            float signoY = vel.y >= 0 ? 1f : -1f;

            vel = new Vector2(signoX * 2f, signoY * 1f).normalized * initialVelocity;
            ballRb.linearVelocity = vel;
        }
    }
    void Tirar() 
    {
        
        var dir = new Vector2(0f, 2f);
        ballRb.linearVelocity = dir.normalized * initialVelocity;
        isBallMoving = true;
        transform.parent = null;
    }
    
}

    
