using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public static bool pauseMenuActive;

    public GameObject pauseMenu;
    public GameObject crosshair;

    [Space]
    public GameObject eventSystem;

    private AudioSource audioSource;

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

    public void Resume()
    {
        ToggleActive();
    }

    public void Options()
    {
        audioSource.Play();
    }

    public void Exit()
    {
        audioSource.Play();
        Fade.instance.SetColor(Fade.ColorName.Default);
        Fade.instance.FadeOut(() => GameState.LoadLevel(0));
    }
}
