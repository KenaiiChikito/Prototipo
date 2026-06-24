using UnityEngine;

public class Bala : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 10f;
    public float lifeTime = 3f;
    public float alcance = 50f; // Agregado para que la bala tenga un alcance definido

    private void Start()
    {
        Destroy(gameObject, lifeTime); // Destruye la bala después de un tiempo
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime); // Mueve la bala hacia adelante
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return; // Evita que la bala dañe al jugador
        // Intenta obtener el componente Vida del objeto impactado
        Vida vida = other.GetComponent<Vida>();
        if (vida != null)
        {
            vida.RecibirDaño(damage); // Aplica daño al objeto impactado
        }

        Destroy(gameObject); // Destruye la bala al impactar con cualquier objeto
    }
}
