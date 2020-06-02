using UnityEngine;
using UnityEngine.EventSystems;

public class MenuUI : MonoBehaviour
{
    private static GameObject currentView;
    private static GameObject previousView;

    public static bool freshStart = true;

    public GameObject defaultView;
    public GameObject startView;

    [Space]
    public GameObject newGameView;
    public GameObject continueView;

    [Space]
    public GameObject inputView;
    private System.Action action;

    private AudioSource audioSource;

    void Start()
    {
        if (freshStart)
        {
            currentView = startView;
            ClientSettings.Load();
            GameState.Load();

            if (Screen.currentResolution.refreshRate > 60)
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 60;
            }    
        }   
        else
            currentView = defaultView;

        currentView.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SetSelected();

        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeView(GameObject view)
    {
        audioSource.Play();
        currentView.SetActive(false);
        currentView = view;
        currentView.SetActive(true);

        SetSelected();
    }

    private void SetSelected()
    {
        var firstButton = currentView.transform.GetChild(0).gameObject;
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    public void Play()
    {
        audioSource.Play();
        if (GameState.noActiveSave)
            ChangeView(newGameView);
        else
            ChangeView(continueView);
    }

    public void NewGame()
    {
        audioSource.Play();
        Fade.instance.SetColor(Fade.ColorName.Default);
        action = () => GameState.NewGame();

        if (GameState.noActiveSave)
            previousView = newGameView;
        else
            previousView = continueView;

        ChangeView(inputView);
    }

    public void Continue()
    {
        audioSource.Play();
        Fade.instance.SetColor(Fade.ColorName.Light);
        action = () => GameState.LoadSavedLevel();
        
        previousView = continueView;
        ChangeView(inputView);
    }

    public void Enter(bool isGamepadSelected)
    {
        audioSource.Play();
        CaptionText.gamepadInputActive = isGamepadSelected;
        Fade.instance.FadeOut(action);
    }

    public void Back()
    {
        audioSource.Play();
        ChangeView(previousView);
    }

    public void Quit()
    {
        audioSource.Play();
        Fade.instance.SetColor(Fade.ColorName.Default);
        Fade.instance.FadeOut(() => Application.Quit());
    }
}