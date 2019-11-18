using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public Camera initialPortalCamera;

    private static Camera activePortalCamera;

    private void Start()
    {
        activePortalCamera = initialPortalCamera;
        activePortalCamera.enabled = true;
    }

    public static void SetActiveCamera(Camera portalCamera)
    {
        activePortalCamera.enabled = false;
        portalCamera.enabled = true;
        activePortalCamera = portalCamera;
    }
}
