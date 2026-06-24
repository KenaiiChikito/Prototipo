using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public Transform target; // El objetivo que la cámara seguirá
    public Vector3 offset = new Vector3(0, 10, -5); // Desplazamiento de la cámara respecto al objetivo
    public float smoothTime = 0.125f; // Tiempo para suavizar el movimiento

    void LateUpdate()
    {
        if (target == null) return; // Asegura que haya un objetivo asignado

        Vector3 desiredPosition = target.position + offset; // Posición deseada de la cámara
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothTime); // Suaviza la transición
        transform.position = smoothedPosition; // Actualiza la posición de la cámara

        transform.LookAt(target); // Hace que la cámara mire al objetivo
    }
}
