using UnityEngine;
using Photon.Pun;

public class PlayerShooting : MonoBehaviourPunCallbacks
{
    public GameObject bulletPrefab; // Arrastra el prefab de la carpeta Resources aquí
    public Transform firePoint;
    public float fireRate = 0.2f;
    private float nextFireTime;

    void Update()
    {
        // Solo yo puedo disparar con mi personaje
        if (!PhotonNetwork.InRoom || !photonView.IsMine) return;

        // "Fire1" es el click izquierdo del mouse
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("Falta el prefab de la bala o el FirePoint en el inspector");
            return;
        }

        // IMPORTANTE: El nombre debe coincidir con el archivo en la carpeta Resources
        PhotonNetwork.Instantiate(bulletPrefab.name, firePoint.position, firePoint.rotation);
    }
}