using UnityEngine;
using Photon.Pun;

public class WallHealth : MonoBehaviourPunCallbacks
{
    public float health = 50f;
    public GameObject destructionEffect; // Opcional: partículas de escombros

    [PunRPC]
    public void TakeWallDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            DestroyWall();
        }
    }

    void DestroyWall()
    {
        // Solo el Master Client (el dueño de la sala) puede destruir objetos del escenario
        if (PhotonNetwork.IsMasterClient)
        {
            if (destructionEffect != null)
            {
                // Creamos el efecto para todos
                PhotonNetwork.Instantiate(destructionEffect.name, transform.position, transform.rotation);
            }

            PhotonNetwork.Destroy(gameObject);
        }
    }
}