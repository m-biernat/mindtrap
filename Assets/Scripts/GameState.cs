using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public static bool noActiveSave = true;
    private static int savedLevelIndex = 0;

    private const string NO_ACTIVE_SAVE_K = "noActiveSave",
                         SAVED_LEVEL_INDEX_K = "savedLevelIndex";

    public static void Load()
    {
        if (PlayerPrefs.HasKey(NO_ACTIVE_SAVE_K))
        {
            noActiveSave = Convert.ToBoolean(PlayerPrefs.GetInt(NO_ACTIVE_SAVE_K));
            savedLevelIndex = PlayerPrefs.GetInt(SAVED_LEVEL_INDEX_K);
        }
    }
    public static void Save()
    {
        PlayerPrefs.SetInt(NO_ACTIVE_SAVE_K, Convert.ToInt32(noActiveSave));
        PlayerPrefs.SetInt(SAVED_LEVEL_INDEX_K, savedLevelIndex);
    }

    public static void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);
    }

    public static void LoadSavedLevel()
    {
        SceneManager.LoadScene(savedLevelIndex, LoadSceneMode.Single);
    }

    public static void LoadNextLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex;

        index++;

        if (index >= SceneManager.sceneCount)
        {
            noActiveSave = true;
            savedLevelIndex = 0;
        }
        else
        {
            noActiveSave = false;
            savedLevelIndex = index;
        }

        Save();
        LoadSavedLevel();
    }
}
