using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;

public class LocalPlayerVision : MonoBehaviourPun
{
    [Header("Ajustes de Visión")]
    [SerializeField] private float visionRadius = 5f;
    [SerializeField] private LayerMask targetMask;    // Capa de los Players y NPCs (ej. "Targets")
    [SerializeField] private LayerMask obstacleMask;  // Capa de las paredes/obstáculos (ej. "Obstacles")
    [SerializeField] private float updateInterval = 0.1f; // Frecuencia de actualización (segundos) para optimizar

    private List<VisibilityController> currentlyVisibleTargets = new List<VisibilityController>();
    private float nextUpdateTime;

    void Start()
    {
        // Solo el jugador que nosotros controlamos ejecuta esta lógica.
        // En los clones de otros jugadores de la red, desactivamos este script.
        if (!photonView.IsMine)
        {
            enabled = false;
            return;
        }
    }

    void Update()
    {
        if (Time.time >= nextUpdateTime)
        {
            nextUpdateTime = Time.time + updateInterval;
            PerformVisionCheck();
        }
    }

    void PerformVisionCheck()
    {
        HashSet<VisibilityController> targetsInSight = new HashSet<VisibilityController>();

        // 1. Buscamos colisionadores dentro del radio del círculo de luz
        Collider[] targetsInRadius = Physics.OverlapSphere(transform.position, visionRadius, targetMask);

        foreach (Collider targetCollider in targetsInRadius)
        {
            // Evitamos detectarnos a nosotros mismos
            if (targetCollider.gameObject == this.gameObject)
                continue;

            // Buscamos si el objetivo tiene el script de visibilidad
            VisibilityController targetVisibility = targetCollider.GetComponent<VisibilityController>();
            if (targetVisibility == null) 
                targetVisibility = targetCollider.GetComponentInParent<VisibilityController>();

            if (targetVisibility != null)
            {
                // 2. Calculamos dirección y distancia hacia el centro del objetivo
                Vector3 targetCenter = targetCollider.bounds.center;
                Vector3 directionToTarget = (targetCenter - transform.position).normalized;
                float distanceToTarget = Vector3.Distance(transform.position, targetCenter);

                // 3. Lanzamos un Raycast. Si no choca con ningún obstáculo (pared), lo vemos
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    targetsInSight.Add(targetVisibility);
                }
            }
        }

        // 4. Mostramos los objetivos que están en nuestro campo de visión actual
        foreach (VisibilityController target in targetsInSight)
        {
            target.SetVisible(true);
            if (!currentlyVisibleTargets.Contains(target))
            {
                currentlyVisibleTargets.Add(target);
            }
        }

        // 5. Ocultamos los que ya no podemos ver (salieron del rango o se ocultaron tras una pared)
        for (int i = currentlyVisibleTargets.Count - 1; i >= 0; i--)
        {
            VisibilityController target = currentlyVisibleTargets[i];
            if (target != null && !targetsInSight.Contains(target))
            {
                target.SetVisible(false);
                currentlyVisibleTargets.RemoveAt(i);
            }
        }
    }

    // Dibuja el rango de visión en la vista de escena para facilitar las pruebas
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
    }
}