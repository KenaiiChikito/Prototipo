using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPunCallbacks
{
    public float speed = 20f;
    public float damage = 10f;
    public float lifeTime = 2f;

    void Start()
    {
        // La bala se destruye sola después de X segundos si no choca con nada
        if (photonView.IsMine)
        {
            Invoke("DestroyBullet", lifeTime);
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
{
    if (!photonView.IsMine) return;

    // 1. Si le pega a un Jugador
    Health playerHealth = other.GetComponent<Health>();
    if (playerHealth != null)
    {
        playerHealth.photonView.RPC("TakeDamage", RpcTarget.All, damage);
    }

    // 2. Si le pega a una Muralla
    WallHealth wall = other.GetComponent<WallHealth>();
    if (wall != null)
    {
        wall.photonView.RPC("TakeWallDamage", RpcTarget.All, damage);
    }

    // Destruir la bala siempre que toque algo sólido
    DestroyBullet();
}
        // Solo el dueño de la bala procesa el choque para evitar doble daño
        if (!photonView.IsMine) return;

        Health targetHealth = other.GetComponent<Health>();
        if (targetHealth != null)
        {
            // Llamamos a la función de daño a través de la red (RPC)
            targetHealth.photonView.RPC("TakeDamage", RpcTarget.All, damage);
        }

        // Si choca con cualquier cosa que no sea el disparador, se destruye
        if (!other.CompareTag("Player") || (other.GetComponent<PhotonView>() && !other.GetComponent<PhotonView>().IsMine))
        {
            DestroyBullet();
        }
    }
    

    void DestroyBullet()
    {
        PhotonNetwork.Destroy(gameObject);
    }
    
}