using UnityEngine;
using UnityEngine.UI;
using System;

public class Fade : MonoBehaviour
{
    public static Fade instance;

    private Animator animator;
    private Image background;

    private Action action;

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
}
