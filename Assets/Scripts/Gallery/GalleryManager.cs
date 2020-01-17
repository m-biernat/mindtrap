using UnityEngine;

public class GalleryManager : MonoBehaviour
{
    public RenderTexture renderTexture;

    public Exhibition startingExhibition;

    void Start()
    {
        renderTexture.width = Screen.height;
        renderTexture.height = Screen.height;

        startingExhibition.enabled = true;
    }
}
