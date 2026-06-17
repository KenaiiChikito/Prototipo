using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    
    public GameObject playerPrefab;
    public Transform[] spawnPoints; // Arrastra 4 GameObjects aquí en el Inspector

    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            // PhotonNetwork.LocalPlayer.ActorNumber nos da el orden de entrada (1, 2, 3 o 4)
            // Restamos 1 para que sea (0, 1, 2, 3) y coincida con el Array
            int index = PhotonNetwork.LocalPlayer.ActorNumber - 1;

            // Por si acaso entran más de los planeados, usamos el operador % (módulo)
            int spawnIndex = index % spawnPoints.Length;

            Transform selectedSpawn = spawnPoints[spawnIndex];

            PhotonNetwork.Instantiate(playerPrefab.name, selectedSpawn.position, selectedSpawn.rotation);
        }
        // Esto hará que en la Sala A el Player 1 salga en el sitio 0, 
        // pero en la Sala B el Player 1 quizás salga en el sitio 2.
        int roomSeed = PhotonNetwork.CurrentRoom.Name.GetHashCode();
        Random.InitState(roomSeed); 
        // (Esto es más avanzado, por ahora te recomiendo el sistema de ActorNumber fijo 
        // para que pruebes que el multijugador funciona bien).
    }
}
