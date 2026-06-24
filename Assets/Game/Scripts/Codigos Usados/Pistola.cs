using UnityEngine;

public class Pistola : MonoBehaviour
{
    public GameObject balaPrefab;
    public float daño = 25f;
    public float alcance = 50f;
    public Transform puntoDisparo;
    public float tiempoEntreDisparos = 0.1f; // Bajado un poco para que sea más ágil
    private float nextTimeToFire = 0f;

    [SerializeField] private LayerMask capasAIgnorar; // Para no dispararte a ti mismo

    void Update()
    {
        // CAMBIO: Usamos GetMouseButton (sin el Down) para que si dejas presionado, 
        // dispare en cuanto el cooldown lo permita. O haz clics rápidos y no se perderán.
        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + tiempoEntreDisparos;
            Disparar();
        }
    }

    void Disparar()
    {
        GameObject nuevaBala = Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
        Bala scriptBala = nuevaBala.GetComponent<Bala>();
        if (scriptBala != null)
        {
            scriptBala.damage = daño; // Asegura que la bala tenga el daño correcto
            scriptBala.alcance = alcance; // Asegura que la bala tenga el alcance correcto
        }
        // El Raycast ahora ignora la capa del Jugador
        RaycastHit hit;
        Vector3 direction = puntoDisparo.forward;

        if (Physics.Raycast(puntoDisparo.position, direction, out hit, alcance, ~capasAIgnorar))
        {
            Debug.Log("Impacto en: " + hit.transform.name);
            Vida sistemaVida = hit.transform.GetComponent<Vida>();
            if (sistemaVida != null)
            {
                sistemaVida.RecibirDaño(daño);
            }
        }
        
        Debug.DrawRay(puntoDisparo.position, direction * alcance, Color.red, 0.1f);
        Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation); // Instancia la bala para efectos visuales, aunque el daño se maneja con el Raycast
    }
    }