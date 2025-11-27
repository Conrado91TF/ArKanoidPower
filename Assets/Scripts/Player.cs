using UnityEngine;

public class Player : MonoBehaviour
{
    // [SerializeField] permite que estas variables se muestren en el Inspector de Unity
    // para que puedas ajustarlas fácilmente sin modificar el código.

    [SerializeField]
    private float moveSpeed = 5f; // Velocidad de movimiento de la paleta.

    [SerializeField]
    private float minX = -7.5f;   // Límite más a la izquierda que la paleta puede ir.
                                  // Ajusta este valor en el Inspector.

    [SerializeField]
    private float maxX = 7.5f;    // Límite más a la derecha que la paleta puede ir.
                                  // Ajusta este valor en el Inspector.

    void Update()
    {
        // 1. Obtener la entrada del usuario para el movimiento horizontal.
        // Input.GetAxisRaw("Horizontal") devuelve -1 (izquierda), 0 (quieto) o 1 (derecha).
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // 2. Calcular el desplazamiento horizontal en este frame.
        // Se multiplica la entrada por la velocidad y por Time.deltaTime
        // para asegurar que el movimiento sea independiente de la velocidad de los frames.
        float horizontalMovement = horizontalInput * moveSpeed * Time.deltaTime;

        // 3. Calcular la nueva posición deseada en el eje X.
        float newXPosition = transform.position.x + horizontalMovement;

        // 4. Restringir la nueva posición X dentro de los límites definidos (minX y maxX).
        // Mathf.Clamp asegura que 'newXPosition' nunca sea menor que 'minX' ni mayor que 'maxX'.
        newXPosition = Mathf.Clamp(newXPosition, minX, maxX);

        // 5. Aplicar la nueva posición a la paleta.
        // Se mantiene la posición Y y Z actual, solo se cambia la X.
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
    }
}

