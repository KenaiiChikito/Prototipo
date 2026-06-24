using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject winPanel;   
    public GameObject pausePanel; 
    private bool isPaused = false;

    void Start()
    {
        // Al iniciar la partida, ocultamos el mouse pero lo dejamos funcional
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !winPanel.activeSelf)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);

        if (isPaused)
        {
            // EN PAUSA: Mouse visible para clickear botones
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f; // Opcional: frena el tiempo local
        }
        else
        {
            // JUGANDO: Mouse invisible pero NO bloqueado para que el Raycast funcione
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None; 
            Time.timeScale = 1f;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            ShowWinUI();
        }
    }

    void ShowWinUI()
    {
        pausePanel.SetActive(false);
        winPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnClick_Leave()
    {
        Time.timeScale = 1f;
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnClick_Quit()
    {
        Application.Quit();
    }
}