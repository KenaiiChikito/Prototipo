using UnityEngine;

public class MinimapCameraFollow : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        if (target != null)
        {
            // La cámara sigue al jugador en X y Z, pero se queda arriba en Y
            transform.position = new Vector3(target.position.x, 100f, target.position.z);
        }
    }
}