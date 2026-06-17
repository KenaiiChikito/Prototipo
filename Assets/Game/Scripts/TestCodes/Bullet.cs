using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPunCallbacks
{
    public float speed = 20f;
    public float damage = 10f;
    public float lifeTime = 2f;

    void Start()
    {
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
        if (!photonView.IsMine) return;

        Health targetHealth = other.GetComponent<Health>();

        // Validamos que el objeto tenga el componente Health y cuente con un PhotonView para enviar el RPC
        if (targetHealth != null && targetHealth.photonView != null)
        {
            targetHealth.photonView.RPC("TakeDamage", RpcTarget.All, damage);
        }

        // Si choca con algo que no sea el jugador que la disparó, se destruye
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