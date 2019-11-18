using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public Camera initialPortalCamera;
    public Material portalCameraMaterial;

    private static Camera activePortalCamera;
    private static Material cameraMaterial;
    private static RenderTexture renderTexture;

    private void Start()
    {
        activePortalCamera = initialPortalCamera;
        activePortalCamera.enabled = true;

        renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        activePortalCamera.targetTexture = renderTexture;

        cameraMaterial = portalCameraMaterial;
        cameraMaterial.mainTexture = activePortalCamera.targetTexture;
    }

    public static void SetActiveCamera(Camera portalCamera)
    {
        activePortalCamera.enabled = false;
        portalCamera.enabled = true;
        activePortalCamera = portalCamera;

        activePortalCamera.targetTexture = renderTexture;
        cameraMaterial.mainTexture = portalCamera.targetTexture;
    }
}
