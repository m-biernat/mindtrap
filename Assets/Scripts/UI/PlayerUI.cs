using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public static bool pauseMenuActive;

    public GameObject pauseMenu;
    public GameObject crosshair;

    [Space]
    public GameObject defaultView;
    public GameObject settingsView;

    [Space]
    public GameObject eventSystem;

    [HideInInspector]
    public AudioSource audioSource;

    private void Start()
    {
        pauseMenuActive = false;
        SetCursorLock();

        if (!crosshair)
            crosshair = new GameObject();

        Instantiate(eventSystem);

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleActive();
        }
    }

    public void Init()
    {
        Instantiate(gameObject);
    }

    private void ToggleActive()
    {
        audioSource.Play();
        
        pauseMenuActive = !pauseMenuActive;
        pauseMenu.SetActive(pauseMenuActive);
        crosshair.SetActive(!pauseMenuActive);
        
        SetCursorLock();
    }

    private void SetCursorLock()
    {
        if (pauseMenuActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ToggleView()
    {
        defaultView.SetActive(!defaultView.activeSelf);
        settingsView.SetActive(!settingsView.activeSelf);
    }

    public void Resume()
    {
        ToggleActive();
    }

    public void Settings()
    {
        audioSource.Play();
        ToggleView();
    }

    public void Exit()
    {
        audioSource.Play();
        Fade.instance.SetColor(Fade.ColorName.Default);
        Fade.instance.FadeOut(() => GameState.LoadLevel(0));
    }
}
