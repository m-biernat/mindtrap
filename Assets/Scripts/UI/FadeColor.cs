using UnityEngine;

public class FadeColor : MonoBehaviour
{
    public Fade.ColorName color;
    
    private void Update()
    {
        if (Fade.instance)
        {
            Fade.instance.SetColor(color);
            enabled = false;
        }
    }
}
