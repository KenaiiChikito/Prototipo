using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform[] spawnPoints; 

    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            int index = PhotonNetwork.LocalPlayer.ActorNumber - 1;
            int spawnIndex = index % spawnPoints.Length;
            Transform selectedSpawn = spawnPoints[spawnIndex];

            // Instanciamos
            GameObject myPlayer = PhotonNetwork.Instantiate(playerPrefab.name, selectedSpawn.position, selectedSpawn.rotation);

            // SEGURIDAD: Solo si se creó con éxito, buscamos el minimapa
            if (myPlayer != null)
            {
                MinimapFollow minimap = Object.FindFirstObjectByType<MinimapFollow>();
                if (minimap != null) 
                {
                    minimap.target = myPlayer.transform;
                }
            }
        }
    }
}