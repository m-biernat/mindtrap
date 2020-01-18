using UnityEngine;
using System;

public class Door : MonoBehaviour
{
    private Transform player;
    private float maxDistance;

    [Range(0.0f, 100.0f)]
    public float maxLuminance;
    private float minLuminance;

    private float distance;
    private float luminance;

    public Renderer m_renderer;

    private const string FLOAT_NAME = "Vector1_EC86FC9E";

    public DoorTrigger trigger;

    public Action teleportAction;

    public enum Selection
    {
        ChangeLevel,
        Teleport
    };

    public Selection function;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        maxDistance = GetDistance();

        minLuminance = m_renderer.material.GetFloat(FLOAT_NAME);

        switch (function)
        {
            case Selection.ChangeLevel:
                trigger.action = ChangeLevel;
                break;

            case Selection.Teleport:
                trigger.action = Teleport;
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
        m_renderer.material.SetFloat(FLOAT_NAME, luminance);
    }

    private void ChangeLevel()
    {
        Fade.instance.SetColor(Fade.ColorName.Light);
        Fade.instance.FadeOut(() => GameState.LoadNextLevel());
    }

    private void Teleport()
    {
        Fade.instance.SetColor(Fade.ColorName.Light);
        Fade.instance.FadeOut(teleportAction);
    }
}
