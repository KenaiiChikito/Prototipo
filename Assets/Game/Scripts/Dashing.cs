using UnityEngine;
using Photon.Pun; // Importante si sigues con Photon

public class Dashing : MonoBehaviourPunCallbacks
{
    [Header("Configuración de Dash")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float cooldownTime = 3f;

    [Header("Estado")]
    private bool isDashing = false;
    private float dashEndTime;
    private float cooldownTimer = 0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 1. Solo el dueño del personaje puede controlarlo
        if (photonView != null && !photonView.IsMine) return;

        // 2. Manejar el temporizador del Cooldown
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // 3. Detectar entrada (Shift y que no esté en cooldown y que no esté dasheando ya)
        if (Input.GetKeyDown(KeyCode.LeftShift) && cooldownTimer <= 0 && !isDashing)
        {
            StartDash();
        }

        // 4. Terminar el dash cuando pase el tiempo
        if (isDashing && Time.time >= dashEndTime)
        {
            StopDash();
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            // Aplicamos velocidad constante durante el dash
            // Usamos transform.forward para que sea hacia donde mira el mouse (según tu script anterior)
            rb.linearVelocity = transform.forward * dashSpeed;
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashEndTime = Time.time + dashDuration;
        cooldownTimer = cooldownTime; // Inicia el cooldown justo al empezar el dash
        
        Debug.Log("Dash iniciado. Cooldown de " + cooldownTime + "s empezado.");
    }

    void StopDash()
    {
        isDashing = false;
        // Opcional: Frenar un poco al terminar el dash para que no deslice infinitamente
        rb.linearVelocity = Vector3.zero;
    }
}