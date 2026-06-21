using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject winPanel;   // Panel de victoria
    public GameObject pausePanel; // Panel de pausa
    private bool isPaused = false;

    void Update()
    {
        // Solo detectamos pausa si no hemos ganado ya (para no solapar menús)
        if (Input.GetKeyDown(KeyCode.Escape) && !winPanel.activeSelf)
        {
            TogglePause();
        }
        
    if (pausePanel.activeSelf) return; // No disparar si el menú está abierto
    // ... resto del disparo

    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);

        // Liberar o bloquear el cursor según la pausa
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            // Opcional: Time.timeScale = 0; // Solo afectará a tus animaciones locales
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            // Opcional: Time.timeScale = 1;
        }
    }

    // --- LÓGICA DE JUGADORES (Lo que ya teníamos) ---

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            ShowWinUI();
        }
    }

    void ShowWinUI()
    {
        // Si estábamos en pausa, la cerramos para mostrar la victoria
        pausePanel.SetActive(false);
        
        winPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // --- BOTONES (Compartidos por ambos paneles) ---

    public void OnClick_Leave()
    {
        // Aseguramos que el tiempo vuelva a la normalidad antes de salir
        Time.timeScale = 1;
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