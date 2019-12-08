using UnityEngine;
using UnityEngine.SceneManagement;

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
            currentView = startView;
        else
            currentView = defaultView;

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
        ChangeView(newGameView); // Temp
    }

    public void NewGame()
    {
        ChangeView(continueView); // Temp
    }

    public void Continue()
    {
        ChangeView(newGameView); // Temp
    }

    public void Quit()
    {
        Application.Quit();
    }
}