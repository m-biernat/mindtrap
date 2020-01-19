using UnityEngine;

public class GalleryManager : MonoBehaviour
{
    public RenderTexture renderTexture;

    public Exhibition startingExhibition;

    void Start()
    {
        renderTexture.width = Screen.height;
        renderTexture.height = (int)(Screen.height * 1.5f);

        startingExhibition.enabled = true;
    }
}
