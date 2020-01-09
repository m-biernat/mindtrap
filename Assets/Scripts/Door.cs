using UnityEngine;

public class Door : MonoBehaviour
{
    private Transform player;
    private float maxDistance;

    [Range(0.0f, 100.0f)]
    public float maxLuminance;
    private float minLuminance;

    private float distance;
    private float luminance;

    public new Renderer renderer;

    private const string FLOAT_NAME = "Vector1_EC86FC9E";

    public DoorTrigger trigger;

    public enum Selection
    {
        ChangeLevel,
    };

    public Selection function;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        maxDistance = GetDistance();

        minLuminance = renderer.material.GetFloat(FLOAT_NAME);

        switch (function)
        {
            case Selection.ChangeLevel:
                trigger.action = ChangeLevel;
                break;
        }
    }

    private void LateUpdate()
    {
        CalculateLuminance();
    }

    private float GetDistance()
    {
        return Vector3.Distance(transform.position, player.position);
    }

    private void CalculateLuminance()
    {
        distance = Mathf.Clamp(GetDistance() / maxDistance, 0.0f, 1.0f);

        luminance = Mathf.Clamp(maxLuminance * distance, minLuminance, maxLuminance);
        renderer.material.SetFloat(FLOAT_NAME, luminance);
    }

    private void ChangeLevel()
    {
        Fade.instance.FadeOut(() => GameState.LoadNextLevel());
    }
}
