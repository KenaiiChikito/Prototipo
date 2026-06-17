using UnityEngine;
using Photon.Pun;

public class Health : MonoBehaviourPunCallbacks
{
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Opciones de Destrucción (Opcional)")]
    [SerializeField] private GameObject destroyedEffect; // Efecto visual al destruirse

    void Awake()
    {
        currentHealth = maxHealth;
    }

    [PunRPC]
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name + " recibió " + amount + " de daño. Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Si tiene efecto de destrucción, se genera localmente en cada cliente antes de borrar el objeto
        if (destroyedEffect != null)
        {
            Instantiate(destroyedEffect, transform.position, transform.rotation);
        }

        // Lógica de destrucción en red
        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
        // Si es un objeto de la escena (como una muralla), el Master Client gestiona su destrucción
        else if (PhotonNetwork.IsMasterClient && photonView.IsRoomView)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}