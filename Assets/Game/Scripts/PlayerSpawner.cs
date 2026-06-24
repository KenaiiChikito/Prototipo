using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform[] spawnPoints; 
    
    [Header("Configuración Minimapa")]
    public MinimapCameraFollow miniCamScript; // Arrastra la CamaraMinimapa aquí

    void Start()
    {
        if (!PhotonNetwork.IsConnected) return;

        // 1. Instanciamos al jugador
        int index = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        int spawnIndex = index % spawnPoints.Length;
        Transform selectedSpawn = spawnPoints[spawnIndex];

        GameObject myPlayer = PhotonNetwork.Instantiate(playerPrefab.name, selectedSpawn.position, selectedSpawn.rotation);

        // 2. Solo si soy yo, le damos mi transform a la cámara
        if (myPlayer != null && myPlayer.GetComponent<PhotonView>().IsMine)
        {
            if (miniCamScript != null)
            {
                miniCamScript.target = myPlayer.transform;
                Debug.Log("Minimapa vinculado manualmente.");
            }
            else
            {
                Debug.LogError("¡ERROR!: No has arrastrado la CamaraMinimapa al script PlayerSpawner.");
            }
        }
    }
}