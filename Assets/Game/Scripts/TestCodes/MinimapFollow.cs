using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        if (target != null)
        {
            // Sigue al jugador en X y Z, manteniendo la altura Y fija
            transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
        }
    }
}