using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab; // Arrastra aquí el prefab de tu jugador

    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            // Simplemente instanciamos. No intentamos conectar ni unirnos a salas.
            // El personaje aparece en una posición aleatoria para que no se encimen
            Vector3 randomPos = new Vector3(Random.Range(-5f, 5f), 1f, Random.Range(-5f, 5f));
            PhotonNetwork.Instantiate(playerPrefab.name, randomPos, Quaternion.identity);
        }
        else
        {
            Debug.LogError("¡No estás conectado a Photon! Vuelve desde la escena de Menú.");
        }
    }
}