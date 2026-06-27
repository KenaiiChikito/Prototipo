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