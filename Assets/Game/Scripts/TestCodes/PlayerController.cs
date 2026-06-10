using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;

        if (photonView.IsMine)
        {
            // EL MÍO: Usa física, no es kinematic
            rb.isKinematic = false;

            if (CameraFollow.instance != null)
            {
                CameraFollow.instance.SetTarget(this.transform);
            }
            else
            {
                Debug.LogWarning("No se encontró el script CameraFollow en la escena. Asegúrate de que la cámara tenga el script.");
            }

        }
        else
        {
            // LOS DEMÁS: No usan física localmente
            rb.isKinematic = true;
        }
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        RotateTowardsMouse();
    }

    void FixedUpdate() // El movimiento físico es mejor en FixedUpdate
    {
        if (!photonView.IsMine) return;
        Move();
    }

    void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized;
        
        // Solo aplicamos velocidad si NO es kinematic (evita el error de la consola)
        if (!rb.isKinematic)
        {
            rb.linearVelocity = moveDirection * moveSpeed;
        }
    }

    void RotateTowardsMouse()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 targetPoint = hit.point;
            Vector3 direction = targetPoint - transform.position;
            direction.y = 0f;

            if (direction.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 15f);
            }
        }
    }
}