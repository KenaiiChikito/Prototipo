using UnityEngine;

public class CharacterSounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] pasosClips; // Array por si tienes varios sonidos de pasos

    // Esta es la función que llamará el evento de la animación
    public void PlayFootstep()
    {
        // Si tienes varios sonidos, elige uno al azar para que no sea monótono
        int index = Random.Range(0, pasosClips.Length);
        audioSource.clip = pasosClips[index];

        // Reproduce el sonido
        audioSource.Play();
    }
}