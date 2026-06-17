using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;   

public class RoomItem : MonoBehaviour
{
    public TextMeshProUGUI roomNameText;
    public void SetRoomInfo(RoomInfo info)
    {
        roomNameText.text = info.Name + " (" + info.PlayerCount + "/" + info.MaxPlayers + ")";
    }

    public void OnClickJoin()
    {
        // Cuando tocas el botón, te unes a esa sala por su nombre
        PhotonNetwork.JoinRoom(roomNameText.text.Split(' ')[0]); 
    }
}