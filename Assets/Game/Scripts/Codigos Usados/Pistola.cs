using UnityEngine;

public class Pistola : MonoBehaviour
{
    public GameObject balaPrefab; // Prefab de la bala que se instanciará al disparar
    public float daño = 25f; // Cantidad de daño que la pistola inflige
    public float alcance = 50f; // Alcance máximo de la pistola
    public Transform puntoDisparo; // Punto desde donde se dispara el rayo
    private float tiempoEntreDisparos = 0.5f; // Tiempo mínimo entre disparos
    private float nextTimeToFire = 0f; // Tiempo en el que se puede disparar nuevamente

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire) // Detecta el clic izquierdo del mouse
        {
            nextTimeToFire = Time.time + tiempoEntreDisparos; // Actualiza el tiempo para el próximo disparo
            Disparar();
        }
    }

    void Disparar()
    {
        //Crea una bala instanciando el prefab en el punto de disparo
        Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
        
        // Crea un rayo desde el punto de disparo hacia adelante
        Debug.DrawRay(puntoDisparo.position, puntoDisparo.forward * alcance, Color.red, 1f); // Dibuja el rayo para debug

        RaycastHit hit;
        // Lanza un rayo desde el punto de disparo hacia adelante
        if (Physics.Raycast(puntoDisparo.position, puntoDisparo.forward, out hit, alcance))
        {
            Debug.Log("Disparaste a: " + hit.transform.name); // Muestra el nombre del objeto impactado

            // Intenta obtener el componente Vida del objeto impactado
            Vida vida = hit.transform.GetComponent<Vida>();
            if (vida != null)
            {
                vida.RecibirDaño(daño); // Aplica daño al objeto impactado
            }
        }
    }
}
