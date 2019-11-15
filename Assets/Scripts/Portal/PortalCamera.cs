using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform portal, otherPortal;
    private Transform playerCameraTransform;

    [Space]
    public Camera portalCamera;
    public Material cameraMat;

    private void Start()
    {
        playerCameraTransform = Camera.main.transform;

        if (portalCamera.targetTexture != null)
        {
            portalCamera.targetTexture.Release();
        }
        portalCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMat.mainTexture = portalCamera.targetTexture;
    }

    private void LateUpdate()
    {
        Vector3 playerOffset = playerCameraTransform.position - otherPortal.position;
        portalCamera.transform.position = portal.position + playerOffset;

        float portalRotation = Quaternion.Angle(portal.rotation, otherPortal.rotation);
        Vector3 cameraDirection = Quaternion.AngleAxis(portalRotation, Vector3.up) * playerCameraTransform.forward;
        portalCamera.transform.rotation = Quaternion.LookRotation(cameraDirection, Vector3.up);
    }
}

