using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public static bool pauseMenuActive;

    public GameObject pauseMenu;
    public GameObject crosshair;

    [Space]
    public GameObject eventSystem;

    private void Start()
    {
        pauseMenuActive = false;
        SetCursorLock();

        Instantiate(eventSystem);
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

    }

    public void Exit()
    {
        Fade.instance.SetColor(Fade.ColorName.Gray);
        Fade.instance.FadeOut(() => GameState.LoadLevel(0));
    }
}
