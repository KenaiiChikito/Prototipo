using UnityEngine;
using Photon.Pun;
using UnityEngine.UI; // Opcional si quieres barras de vida

public class Health : MonoBehaviourPunCallbacks
{
    public float maxHealth = 100f;
    public float currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    [PunRPC] // Esto permite que otros jugadores ejecuten esta función en tu cliente
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name + " recibió daño. Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (photonView.IsMine)
        {
            Debug.Log("Has muerto");
            // Aquí puedes añadir lógica de Respawn o destruir el objeto
            PhotonNetwork.Destroy(gameObject);
        }
    }
}