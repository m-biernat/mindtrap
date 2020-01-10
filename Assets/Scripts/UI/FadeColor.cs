using UnityEngine;

public class FadeColor : MonoBehaviour
{
    public Fade.ColorName color;

    public static Fade.ColorName requestedColor = Fade.ColorName.Default;

    private void Update()
    {
        if (Fade.instance)
        {
            if (requestedColor != Fade.ColorName.Default)
            {
                color = requestedColor;
                requestedColor = Fade.ColorName.Default;
            }

            Fade.instance.SetColor(color);
            enabled = false;
        }
    }
}
