using UnityEngine;

public class MenuUI : MonoBehaviour
{
    private static GameObject currentView;

    public static bool freshStart = true;

    public GameObject defaultView;
    public GameObject startView;

    [Space]
    public GameObject newGameView;
    public GameObject continueView;

    private AudioSource audioSource;

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

        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeView(GameObject view)
    {
        audioSource.Play();
        currentView.SetActive(false);
        currentView = view;
        currentView.SetActive(true);
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
        Fade.instance.FadeOut(() => GameState.NewGame());
    }

    public void Continue()
    {
        audioSource.Play();
        Fade.instance.SetColor(Fade.ColorName.Light);
        Fade.instance.FadeOut(() => GameState.LoadSavedLevel());
    }

    public void Quit()
    {
        audioSource.Play();
        Fade.instance.FadeOut(() => Application.Quit());
    }
}