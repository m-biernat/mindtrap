﻿using UnityEngine;

public class MenuUI : MonoBehaviour
{
    private static GameObject currentView;

    public static bool freshStart = true;

    public GameObject defaultView;
    public GameObject startView;

    [Space]
    public GameObject newGameView;
    public GameObject continueView;

    void Start()
    {
        if (freshStart)
        {
            currentView = startView;
            ClientSettings.Load();
            GameState.Load();
        }   
        else
        {
            currentView = defaultView;
        }

        currentView.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ChangeView(GameObject view)
    {
        currentView.SetActive(false);
        currentView = view;
        currentView.SetActive(true);
    }

    public void Play()
    {
        if (GameState.noActiveSave)
            ChangeView(newGameView);
        else
            ChangeView(continueView);
    }

    public void NewGame()
    {
        GameState.LoadLevel(1);
    }

    public void Continue()
    {
        GameState.LoadSavedLevel();
    }

    public void Quit()
    {
        Application.Quit();
    }
}