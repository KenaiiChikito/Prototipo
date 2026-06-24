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
            // 1. Lógica de selección de Spawn por número de jugador
            int index = PhotonNetwork.LocalPlayer.ActorNumber - 1;
            int spawnIndex = index % spawnPoints.Length;
            Transform selectedSpawn = spawnPoints[spawnIndex];

            // 2. INSTANCIAR AL JUGADOR (Solo una vez)
            // Guardamos el objeto creado en la variable 'myPlayer'
            GameObject myPlayer = PhotonNetwork.Instantiate(playerPrefab.name, selectedSpawn.position, selectedSpawn.rotation);

            // 3. INTEGRACIÓN CON EL MINIMAPA
            // Como acabamos de instanciarlo nosotros, IsMine siempre será true para este objeto
            if (myPlayer.GetComponent<PhotonView>().IsMine)
            {
                // Buscamos el script MinimapFollow que creamos antes
                // En Unity 6 se recomienda usar FindFirstObjectByType
                MinimapFollow minimap = Object.FindFirstObjectByType<MinimapFollow>();
                
                if (minimap != null) 
                {
                    minimap.target = myPlayer.transform;
                    Debug.Log("Minimapa configurado para seguir a: " + myPlayer.name);
                }
                else
                {
                    Debug.LogWarning("No se encontró el script MinimapFollow en la escena.");
                }
            }
        }
    }
}