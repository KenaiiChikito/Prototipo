using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform camTransform;

    void Start()
    {
        // Buscamos la cámara principal
        if (Camera.main != null) camTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (camTransform == null) return;

        // ESTO ES LO DEFINITIVO:
        // En lugar de mirar a la cámara, simplemente COPIAMOS su rotación.
        // Como la cámara del juego no gira (solo se mueve), 
        // la barra se quedará siempre horizontal y mirando al frente.
        transform.rotation = camTransform.rotation;
    }
}