using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

public class PlayerShooting : MonoBehaviourPunCallbacks
{
    public GameObject bulletPrefab; 
    public Transform firePoint;
    public float fireRate = 0.2f;
    private float nextFireTime;

    void Update()
    {
        if (!PhotonNetwork.InRoom || !photonView.IsMine) return;

        // Bloqueo de disparo si el menú de pausa está abierto
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        // Instanciamos la bala con la posición y rotación del FirePoint.
        // Como el FirePoint es hijo del jugador, siempre mirará a donde mira el jugador.
        PhotonNetwork.Instantiate(bulletPrefab.name, firePoint.position, firePoint.rotation);
    }
}