using Unity.VisualScripting;
using UnityEngine;

public enum MirarMouse
{
    CameraDirection,
    MovingDirection,
    MousePosition, // <-- Nuevo estado
}

public class CodigoMovimiento : MonoBehaviour
{

    //Funciones de camara para que este mas clean

    public float rotationSmoothTime = 0.1f; // Tiempo para suavizar la rotación
    private float currentRotationVelocity; // Velocidad actual de rotación (usada por SmoothDampAngle)
    
    private Camera cam;
    //private Animator animator;
    private Rigidbody rb;
    public float speed = 6f;
    private Vector3 smoothDirection = Vector3.zero;
    private float tValue = 6f; //animation transition value
    public float rotationVelocity = 500f;
    private Vector3 direction;
    public MirarMouse lookAt;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cam = Camera.main;
        
        // Actualizacion para recordar que hice: Para ver el mouse, usamos Confined o None
        if (lookAt == MirarMouse.MousePosition) {
            Cursor.lockState = CursorLockMode.Confined; // Se ve el mouse pero no sale de la ventana
            Cursor.visible = false; // Asegura que el cursor sea visible
        } else {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; // El cursor no se ve 
        }
    }

    void Update()
    {
        float haxis = Input.GetAxisRaw("Horizontal");
        float vaxis = Input.GetAxisRaw("Vertical");

        //Presionar ESC para mostar el mouse y liberar el bloqueo de este mismo
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
  
        }
        //Si se hace click en la pantalla vuelve a bloquear el mouse y ocultarse
        if (Input.GetMouseButtonDown(0) && lookAt == MirarMouse.MousePosition)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }


        
        // El movimiento sigue dependiendo de la cámara
        direction = cam.transform.rotation * new Vector3(haxis, 0, vaxis);
        direction.y = 0;

        // Movimiento
        rb.linearVelocity = new Vector3(direction.x, 0, direction.z).normalized * speed + new Vector3(0, rb.linearVelocity.y, 0);

        if (Input.GetMouseButtonDown(0))
            //animator.SetTrigger("Attack");

        //animator.SetBool("Defend", Input.GetMouseButton(1));

        // LÓGICA DE ROTACIÓN SEGÚN EL MODO
        if (lookAt == MirarMouse.CameraDirection)
        {
            smoothDirection = Vector3.MoveTowards(smoothDirection, new Vector3(haxis, 0, vaxis), Time.deltaTime * tValue);
            ChangeDirection(cam.transform.forward);
        }
        else if (lookAt == MirarMouse.MovingDirection)
        {
            smoothDirection = Vector3.MoveTowards(smoothDirection, new Vector3(0, 0, direction.magnitude), Time.deltaTime * tValue);
            ChangeDirection(direction);
        }
        else if (lookAt == MirarMouse.MousePosition)
        {
            // En este modo, el cilindro siempre mira al mouse
            MirarAlMouse();
            
            // Para la animación: calculamos la dirección relativa a donde mira el personaje
            // Esto permite que camine de lado o hacia atrás si el mouse está en otra dirección
            Vector3 relativeDir = transform.InverseTransformDirection(direction);
            smoothDirection = Vector3.MoveTowards(smoothDirection, relativeDir, Time.deltaTime * tValue);
        }

        //animator.SetFloat("ejeX", smoothDirection.x);
        //animator.SetFloat("ejeZ", smoothDirection.z);
    }

    void MirarAlMouse()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position); // Plano a la altura del jugador
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 pointToLook = ray.GetPoint(rayDistance);
            Vector3 lookDir = pointToLook - transform.position;
            lookDir.y = 0; // Evita que el cilindro se incline

            if (lookDir.magnitude > 0.5f) // Solo rota si el mouse está a una distancia mínima para evitar jitter
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDir);
                // Usamos la misma velocidad de rotación que definiste
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            }
        }
    }

    void ChangeDirection(Vector3 directionToLook)
    {
        // Solo rota si hay movimiento (para Camera o Moving direction)
        if (direction.magnitude > 0)
        {
            directionToLook.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(directionToLook);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationVelocity);
        }
    }

    void FixedUpdate()
    {
        float currentMovSpeed = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).magnitude;
        currentMovSpeed = Mathf.Clamp(currentMovSpeed, 0, speed);

        if (direction.magnitude > 0.5f)
            rb.linearVelocity = new Vector3(direction.x, 0, direction.z).normalized * currentMovSpeed + new Vector3(0, rb.linearVelocity.y, 0);
    }
}