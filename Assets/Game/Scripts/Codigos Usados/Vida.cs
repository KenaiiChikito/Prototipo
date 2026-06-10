using UnityEngine;

public class Vida : MonoBehaviour
{

    public float vidaMaxima = 100f;
    private float vidaActual;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void RecibirDaño(float daño)
    {
        vidaActual -= daño;
        Debug.Log(gameObject.name + " recibió " + daño + " de daño. Vida actual: " + vidaActual);
        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log(gameObject.name + " ha muerto.");
        // Aquí puedes agregar lógica adicional, como reproducir una animación de muerte, desactivar el objeto, etc.
        Destroy(gameObject); // Destruye el objeto del juego
    }
}
