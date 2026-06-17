using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro; // Si usas TextMeshPro

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher instance;

    [Header("Lobby Config")]
    public GameObject playerListItemPrefab; // Arrastra tu prefab de texto aquí
    public Transform playerListContent;      // Arrastra el objeto con el Vertical Layout Group

    [Header("Paneles UI")]
    public GameObject loadingPanel;
    public GameObject menuPanel;
    public GameObject lobbyPanel;

    [Header("UI Textos")]
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI roomNameText;
    public GameObject startButton; // El botón de "Iniciar Partida"
    public TMPro.TMP_InputField nameInputField;
    [Header("Room Browser")]
    public GameObject roomItemPrefab;
    public Transform roomListContent;

    void Awake()
    {
        instance = this;
    }

    void Start()
{
    // Sincronizar escenas es fundamental
    PhotonNetwork.AutomaticallySyncScene = true;

    // DIAGNÓSTICO: Esto te dirá en la consola en qué estado estás exactamente
    Debug.Log("Estado actual de Photon: " + PhotonNetwork.NetworkClientState);

    // Verificamos el estado antes de intentar cualquier conexión
    if (PhotonNetwork.IsConnected)
    {
        Debug.Log("Ya estábamos conectados. Yendo al menú directamente.");
        ShowPanel(menuPanel); // Si ya está conectado, salta la carga
    }
    else
    {
        Debug.Log("No conectado. Iniciando conexión...");
        ShowPanel(loadingPanel);
        
        // Configuramos la versión del juego (esto ayuda a separar versiones)
        PhotonNetwork.GameVersion = "1.0"; 
        PhotonNetwork.ConnectUsingSettings();
    }
    
}
public void OnClickCreate() 
{
    SetNickname(); // <--- IMPORTANTE: Guardar el nombre antes de entrar
    CreateRoom();
}
    // Este método borra la lista vieja y escribe la nueva
void UpdatePlayerList()
{
    // Borrar los textos viejos para no duplicar
    foreach (Transform child in playerListContent)
    {
        Destroy(child.gameObject);
    }

    // Crear un texto por cada jugador en la sala
    foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
    {
        GameObject newListItem = Instantiate(playerListItemPrefab, playerListContent);
        newListItem.GetComponent<TMPro.TextMeshProUGUI>().text = player.NickName;
    }
}

    // --- CALLBACKS DE PHOTON ---

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado al Master");
        PhotonNetwork.JoinLobby(); // Entrar al lobby principal para ver salas
    }

    public override void OnJoinedLobby()
    {
        ShowPanel(menuPanel);
        Debug.Log("En el Lobby");
    }

    public override void OnJoinedRoom()
    {
        // Cuando entramos a una sala (creada o unida)
        ShowPanel(lobbyPanel);
        UpdatePlayerList(); // Actualiza la lista de jugadores en la UI
        roomNameText.text = "Sala: " + PhotonNetwork.CurrentRoom.Name;
        
        // Solo el que creó la sala puede ver el botón de iniciar
        UpdateStartButton();
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
{
    UpdatePlayerList(); // Actualizar cuando OTRO entra
}

public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
{
    UpdatePlayerList(); // Actualizar cuando alguien se va
}

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        // Si el dueño se sale, el nuevo dueño recibe el botón de inicio
        UpdateStartButton();
    }
    // Este método de Photon se activa solo cuando hay cambios en las salas
public override void OnRoomListUpdate(System.Collections.Generic.List<Photon.Realtime.RoomInfo> roomList)
{
    // Limpiar lista vieja
    foreach (Transform trans in roomListContent) {
        Destroy(trans.gameObject);
    }

    // Crear un botón por cada sala abierta
    foreach (var room in roomList)
    {
        // Si la sala está llena o cerrada, no la mostramos (opcional)
        if (room.RemovedFromList) continue;

        GameObject newItem = Instantiate(roomItemPrefab, roomListContent);
        newItem.GetComponent<RoomItem>().SetRoomInfo(room);
    }
}

    // --- FUNCIONES DE BOTONES ---

    public void CreateRoom()
    {
        string roomName = "Sala" + Random.Range(0, 1000);
        RoomOptions options = new RoomOptions { MaxPlayers = 4 };
        PhotonNetwork.CreateRoom(roomName, options);
        ShowPanel(loadingPanel);
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
        ShowPanel(loadingPanel);
    }

    public void StartGame()
    {
        // Carga la escena de juego para todos (debe estar en Build Settings)
        PhotonNetwork.LoadLevel("Prototipo"); 
    }

    // --- UTILIDADES ---

    void ShowPanel(GameObject panel)
    {
        loadingPanel.SetActive(false);
        menuPanel.SetActive(false);
        lobbyPanel.SetActive(false);

        panel.SetActive(true);
    }

    void UpdateStartButton()
    {
        // Solo el Master Client (host) puede iniciar el juego
        startButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public void ListPlayers()
{
    foreach (Player player in PhotonNetwork.PlayerList)
    {
        Debug.Log("Jugador: " + player.NickName);
        // Aquí podrías instanciar un texto en la UI con el nombre
    }
    // Este método borra la lista vieja y escribe la nueva

}
public void SetNickname()
{
    if (!string.IsNullOrEmpty(nameInputField.text))
    {
        PhotonNetwork.NickName = nameInputField.text;
        PlayerPrefs.SetString("PlayerName", nameInputField.text); // Opcional: guardarlo para la próxima vez
    }
}
}