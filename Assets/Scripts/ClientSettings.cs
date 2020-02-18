using UnityEngine;

public static class ClientSettings
{
    private const float DEFAULT_M_SENSITIVITY = 2.5f;
    private const float DEFAULT_VOLUME = 0.9f;

    public static float mouseSensitivity = DEFAULT_M_SENSITIVITY;
    public static float volume = DEFAULT_VOLUME;

    private const string MOUSE_SENSITIVITY_K = "mouseSensitivity",
                         VOLUME_K = "volume";

    public static void Load()
    {
        if (PlayerPrefs.HasKey(MOUSE_SENSITIVITY_K))
        {
            mouseSensitivity = PlayerPrefs.GetFloat(MOUSE_SENSITIVITY_K);
            volume = PlayerPrefs.GetFloat(VOLUME_K);

            AudioListener.volume = volume;
        }
    }

    public static void SaveChanges()
    {
        PlayerPrefs.SetFloat(MOUSE_SENSITIVITY_K, mouseSensitivity);
        PlayerPrefs.SetFloat(VOLUME_K, volume);

        AudioListener.volume = volume;
    }

    public static void LoadDefaults()
    {
        mouseSensitivity = DEFAULT_M_SENSITIVITY;
        volume = DEFAULT_VOLUME;

        AudioListener.volume = volume;
    }
}