using UnityEngine;

public class CamaraSeguidora : MonoBehaviour
{
    public Transform objetivo; // Arrastra aquí a tu Jugador
    public Vector3 distancia = new Vector3(0, 10, -5); // Ajusta la altura y distancia
    public float suavidad = 0.125f;

    void LateUpdate()
    {
        if (objetivo == null) return;

        // Posición deseada de la cámara
        Vector3 posicionDeseada = objetivo.position + distancia;
        // Movimiento fluido de la cámara
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, suavidad);
        transform.position = posicionSuavizada;

        // Opcional: Que la cámara siempre apunte al jugador
        transform.LookAt(objetivo.position);
    }
}