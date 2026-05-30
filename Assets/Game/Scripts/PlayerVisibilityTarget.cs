using UnityEngine;

/// <summary>
/// Marca a este jugador como objetivo de visibilidad.
/// Se registra automáticamente en el VisionCone del jugador local.
/// Adjunta a: todos los jugadores NO locales.
/// </summary>
public class PlayerVisibilityTarget : MonoBehaviour
{
    [Tooltip("Punto usado para el raycast de línea de visión. Si es null, usa transform.position")]
    public Transform eyeTransform;

    /// <summary>Posición usada para el chequeo de LOS (línea de visión).</summary>
    public Vector3 EyePosition => eyeTransform != null
        ? eyeTransform.position
        : transform.position;

    private Renderer[] _renderers;
    private VisionCone _vision;

    void Awake()
    {
        // Obtiene todos los renderers del jugador y sus hijos
        _renderers = GetComponentsInChildren<Renderer>(includeInactive: true);
    }

    void Start()
    {
        // Busca el VisionCone del jugador local y se registra
        // En un juego en red, reemplaza este FindFirstObjectByType
        // con tu sistema de networking (ej: NetworkManager.LocalPlayer.GetComponentInChildren<VisionCone>())
        _vision = FindFirstObjectByType<VisionCone>();
        _vision?.RegisterPlayer(this);

        SetVisible(false); // Empieza oculto
    }

    void OnDestroy() => _vision?.UnregisterPlayer(this);

    /// <summary>Activa o desactiva todos los renderers de este jugador.</summary>
    public void SetVisible(bool visible)
    {
        foreach (Renderer r in _renderers)
            r.enabled = visible;
    }
}