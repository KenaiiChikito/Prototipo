using UnityEngine;
using Photon.Pun;

public class PlayerVisionSetup : MonoBehaviourPunCallbacks
{
    [Header("Referencias de Visión")]
    [Tooltip("Arrastra aquí el GameObject que contiene el círculo de luz, el MeshRenderer y el script de Raycasts.")]
    [SerializeField] private GameObject visionSystemObject;

    void Start()
    {
        // Comprobamos si este prefab instanciado nos pertenece
        if (photonView.IsMine)
        {
            // Es nuestro personaje local: activamos su sistema de visión
            if (visionSystemObject != null)
            {
                visionSystemObject.SetActive(true);
            }
        }
        else
        {
            // Es un jugador remoto: desactivamos su sistema de visión en nuestra pantalla
            if (visionSystemObject != null)
            {
                visionSystemObject.SetActive(false);
            }
        }
    }
}