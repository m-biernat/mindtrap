using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Fade : MonoBehaviour
{
    public static Fade instance;

    private Animator animator;
    private Image background;

    private Action action;

    public List<Color> colors;

    public enum ColorName
    { 
        Gray,
        Light
    };

    private void Start()
    {
        instance = this;
        animator = GetComponent<Animator>();
        background = GetComponent<Image>();
    }

    public void FadeIn()
    {
        animator.SetTrigger("fadeIn");
    }

    public void FadeOut(Action onFadeComplete)
    {
        animator.SetTrigger("fadeOut");
        action = onFadeComplete;
    }

    public void OnFadeComplete()
    {
        action.Invoke();
    }

    public void SetColor(ColorName color)
    {
        background.color = colors[(int)color];
    }
}
