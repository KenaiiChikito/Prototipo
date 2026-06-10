using Unity.Mathematics;
using UnityEngine;

public class MIraMouse : MonoBehaviour
{
 // Update se llama una vez por frame
    void Update()
    {
        MirarAlMouse();
    }

    void MirarAlMouse()
    {
        // 1. Crear un rayo desde la cámara que pase por la posición del mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 2. Crear un plano virtual en el suelo (hacia arriba, en la posición del jugador)
        // Esto sirve para que el rayo tenga algo contra qué chocar
        Plane groundPlane = new Plane(Vector3.up, transform.position);

        float rayDistance;

        // 3. Si el rayo choca con el plano...
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            // Obtener el punto exacto del mundo donde el rayo tocó el plano
            Vector3 pointToLook = ray.GetPoint(rayDistance);

            // Dibujar una línea en el editor para debug (opcional)
            Debug.DrawLine(ray.origin, pointToLook, Color.cyan);

            // 4. Hacer que el cilindro mire a ese punto, pero manteniendo la altura fija
            // para que no se incline hacia arriba o abajo si el suelo no está nivelado.
            Vector3 targetDirection = pointToLook - transform.position;
            targetDirection.y = 0; // Bloqueamos el eje Y

            float speed = 5f; // Velocidad de rotación
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
        
    }
}

