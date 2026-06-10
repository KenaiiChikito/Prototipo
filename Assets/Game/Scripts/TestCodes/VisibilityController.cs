using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;

public class VisibilityController : MonoBehaviour
{
    [Header("Configuración Visual")]
    [Tooltip("El modelo 3D o cuerpo principal del jugador.")]
    [SerializeField] private GameObject visualsGroup; 

    [Tooltip("Lista para objetos adicionales que ya están en el personaje (ej. sombreros, mochilas).")]
    [SerializeField] private List<GameObject> extraVisualGroups = new List<GameObject>();

    private PhotonView pv;
    private bool isCurrentlyVisible = true; // Guarda el estado actual de visibilidad

    void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    void Start()
    {
        // Al iniciar, ocultamos todo si es un jugador remoto o NPC
        if (pv == null || !pv.IsMine)
        {
            SetVisible(false);
        }
    }

    public void SetVisible(bool visible)
    {
        isCurrentlyVisible = visible;

        // 1. Modificar el cuerpo principal
        if (visualsGroup != null)
        {
            visualsGroup.SetActive(visible);
        }

        // 2. Modificar todos los elementos extra de la lista
        for (int i = extraVisualGroups.Count - 1; i >= 0; i--)
        {
            if (extraVisualGroups[i] != null)
            {
                extraVisualGroups[i].SetActive(visible);
            }
            else
            {
                // Limpieza por si algún objeto de la lista fue destruido
                extraVisualGroups.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Permite registrar una pistola u otro objeto externo en tiempo de ejecución.
    /// </summary>
    public void RegisterExtraVisual(GameObject extraVisual)
    {
        if (extraVisual != null && !extraVisualGroups.Contains(extraVisual))
        {
            extraVisualGroups.Add(extraVisual);
            // Aplicamos de inmediato el estado de visibilidad actual al nuevo objeto
            extraVisual.SetActive(isCurrentlyVisible);
        }
    }

    /// <summary>
    /// Quita un objeto de la lista si se desequipa o se destruye.
    /// </summary>
    public void UnregisterExtraVisual(GameObject extraVisual)
    {
        if (extraVisual != null && extraVisualGroups.Contains(extraVisual))
        {
            extraVisualGroups.Remove(extraVisual);
        }
    }
}