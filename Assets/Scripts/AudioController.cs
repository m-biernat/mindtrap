using UnityEngine;

public class AudioController : MonoBehaviour
{
    public float volume = 0;

    private void Update()
    {
        SetVolume(volume);
    }

    public static void SetVolume(float volume)
    {
        AudioListener.volume = volume * ClientSettings.volume;
    }
}
