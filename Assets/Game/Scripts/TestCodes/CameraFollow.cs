using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance; // Esto permite que el Player te encuentre fácil

    public Vector3 offset = new Vector3(0f, 15f, -7f); // Ajusta Z si quieres ver más lejos
    public float smoothSpeed = 10f;
    private Transform target;

    void Awake()
    {
        // Al empezar, la cámara se anuncia a sí misma
        instance = this;
    }

    // El jugador llamará a esta función para que la cámara lo siga
    public void SetTarget(Transform playerTransform)
    {
        target = playerTransform;
        Debug.Log("Cámara siguiendo a: " + playerTransform.name);
    }

    void LateUpdate()
    {
        if (target == null) return;

        transform.position = target.position + offset;
        transform.LookAt(target.position);
    }
}