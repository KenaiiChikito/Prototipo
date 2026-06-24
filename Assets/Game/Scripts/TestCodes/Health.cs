using UnityEngine;
using Photon.Pun;
using UnityEngine.UI; // NECESARIO PARA EL SLIDER

public class Health : MonoBehaviourPunCallbacks
{
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI")]
    public Slider healthSlider; // Arrastra el Slider aquí en el inspector

    [Header("Opciones de Destrucción")]
    [SerializeField] private GameObject destroyedEffect;

    void Awake()
    {
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    [PunRPC]
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        
        // Actualizamos la barra visualmente
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
{
    if (photonView.IsMine)
    {
        // Llamamos al RPC del GameManager antes de destruirnos
        // Asegúrate de que el objeto GameManager de tu escena tenga un PhotonView
        FindObjectOfType<GameManager>().photonView.RPC("RPC_CheckPlayers", RpcTarget.All);
        
        PhotonNetwork.Destroy(gameObject);
    }
}
}