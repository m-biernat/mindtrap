using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    private Animator animator;

    public enum CutsceneType
    {
        FallAsleep,
        WakeUp
    };

    public CutsceneType cutscene;

    public float yAxisClamp, xAxisClamp;

    private float yAxisRotation = 0f, xAxisRotation = 0f;
    private float camRotationY = 0f, camRotationX = 0f;

    private Camera cam;
    private float yAxisOffset;

    public float mouseDownturn;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger(GetCutsceneName());

        cam = Camera.main;
        yAxisOffset = cam.transform.rotation.eulerAngles.y;
    }

    private void Update()
    {
        if (PlayerUI.pauseMenuActive)
        {
            animator.enabled = false;
            return;
        }

        animator.enabled = true;

        yAxisRotation = Input.GetAxisRaw("Mouse X");
        xAxisRotation = Input.GetAxisRaw("Mouse Y");

        Vector3 rotation = CalculateRotation();
        rotation.y += yAxisOffset;

        cam.transform.localEulerAngles = rotation;
    }

    private string GetCutsceneName()
    {
        switch (cutscene)
        {
            case CutsceneType.FallAsleep:
                return "fallAsleep";
            case CutsceneType.WakeUp:
                return "wakeUp";
        }

        return "";
    }

    private Vector3 CalculateRotation()
    {
        camRotationX += xAxisRotation * ClientSettings.mouseSensitivity * mouseDownturn;
        camRotationX = Mathf.Clamp(camRotationX, -xAxisClamp, xAxisClamp);

        camRotationY += yAxisRotation * ClientSettings.mouseSensitivity * mouseDownturn;
        camRotationY = Mathf.Clamp(camRotationY, -yAxisClamp, yAxisClamp);

        return new Vector3(-camRotationX, camRotationY, 0f);
    }

    public void FallAsleep()
    {
        FadeColor.requestedColor = Fade.ColorName.Dark;
        
        Fade.instance.SetColor(Fade.ColorName.Dark);
        Fade.instance.FadeOut(() => GameState.LoadNextLevel());
    }

    public void WakeUp()
    {
        GameObject playerUI = GameObject.FindGameObjectWithTag("PlayerUI");
        playerUI.GetComponent<PlayerUI>().enabled = false;
        playerUI.GetComponent<PlayerUI>().pauseMenu.SetActive(false);

        Debug.Log("DING");
    }
}
