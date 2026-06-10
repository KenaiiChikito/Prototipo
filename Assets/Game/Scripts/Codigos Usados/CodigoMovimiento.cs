using UnityEngine;

public enum MirarMouse { CameraDirection, MovingDirection, MousePosition }

public class CodigoMovimiento : MonoBehaviour
{
    private Camera cam;
    private Rigidbody rb;
    
    [Header("Ajustes de Movimiento")]
    public float speed = 6f;
    public MirarMouse lookAt = MirarMouse.MousePosition;

    [Header("Ajustes de Rotación")]
    [Range(1f, 20f)]
    public float suavidadGiro = 10f; // Menos es más lento/suave. Prueba con 5 o 7.

    private Vector3 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        cam = Camera.main;
        ConfigurarCursor();
    }

    void ConfigurarCursor()
    {
        if (lookAt == MirarMouse.MousePosition) {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false; 
        } else {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        // Liberar mouse con ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        float haxis = Input.GetAxisRaw("Horizontal");
        float vaxis = Input.GetAxisRaw("Vertical");

        // Calculamos dirección relativa a la cámara
        direction = cam.transform.rotation * new Vector3(haxis, 0, vaxis);
        direction.y = 0;

        // Movimiento por Rigidbody
        rb.linearVelocity = new Vector3(direction.x * speed, rb.linearVelocity.y, direction.z * speed);

        // Lógica de Rotación
        if (lookAt == MirarMouse.MousePosition)
        {
            ProcesarMirarAlMouse();
        }
        else
        {
            // Para los otros modos usamos la dirección de movimiento
            if (direction.magnitude > 0.1f)
            {
                Quaternion targetRot = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * suavidadGiro);
            }
        }
    }

    void ProcesarMirarAlMouse()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position); 
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 pointToLook = ray.GetPoint(rayDistance);
            Vector3 lookDir = pointToLook - transform.position;
            lookDir.y = 0; 

            // Solo rotar si el mouse no está exactamente encima del jugador
            if (lookDir.magnitude > 0.5f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDir);
                // SLERP es la clave para que no sea brusco
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * suavidadGiro);
            }
        }
    }
}