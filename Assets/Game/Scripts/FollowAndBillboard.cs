using UnityEngine;

public class FollowAndBillboard : MonoBehaviour
{
    public Transform target; // Arrastra aquí a PlayerM
    
    [Header("Ajustes de Posición")]
    public Vector3 offset = new Vector3(0, 2.5f, -0.5f); // Valores X, Y, Z para mover la barra

    private Transform camTransform;

    void Start()
    {
        if (Camera.main != null) camTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (target == null || camTransform == null) return;

        // 1. SEGUIR POSICIÓN CON DESPLAZAMIENTO
        // Sumamos el offset a la posición del jugador
        transform.position = target.position + offset;

        // 2. BLOQUEAR ROTACIÓN
        transform.rotation = camTransform.rotation;
    }
}