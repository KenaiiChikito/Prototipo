using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // 1. Conectamos al servidor
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        // 2. Cuando conecte, entramos a una sala cualquiera
        PhotonNetwork.JoinOrCreateRoom("SalaGeneral", new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        // 3. Cuando estemos en la sala, instanciamos al jugador
        // Asegúrate de que "Player" sea el nombre exacto de tu prefab en Resources
        PhotonNetwork.Instantiate("Player", new Vector3(0, 1, 0), Quaternion.identity);
    }
}