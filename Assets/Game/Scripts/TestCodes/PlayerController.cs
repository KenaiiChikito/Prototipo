using Mono.Cecil;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviourPunCallbacks
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Camera cam;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        animator = GetComponentInChildren<Animator>();

        if (photonView.IsMine)
        {
            rb.isKinematic = false;
            if (CameraFollow.instance != null) CameraFollow.instance.SetTarget(this.transform);
            
            // Mouse invisible pero funcional para el Raycast
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            rb.isKinematic = true;
        }
    }

    void Update()
    {
        if (!PhotonNetwork.InRoom || !photonView.IsMine) return;

        // Si el mouse está sobre la pausa o botones, no rotar
        if (EventSystem.current.IsPointerOverGameObject()) return;

        RotateTowardsMouse();

        bool isWalking = (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0);
        animator.SetBool("Walk", isWalking);

    }

    void FixedUpdate()
    {
        if (!photonView.IsMine) return;
        Move();
    }

    void Move()
    {
        // Movimiento en ejes globales (W = Arriba, S = Abajo, A = Izquierda, D = Derecha)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized;
        
        if (!rb.isKinematic)
        {
            // Aplicamos velocidad manteniendo la gravedad (linearVelocity.y)
            rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);
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
                // El personaje gira hacia el punto del impacto del mouse
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 20f);
            }
        }
    }
}