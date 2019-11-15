using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PortalCamera : MonoBehaviour
{
    public Transform portal, otherPortal;
    private Transform playerCamera;

    [Space]
    public Material cameraMat;
    private Camera portalCamera;

    private void Start()
    {
        playerCamera = Camera.main.transform;
        portalCamera = GetComponent<Camera>();

        if (portalCamera.targetTexture != null)
        {
            portalCamera.targetTexture.Release();
        }
        portalCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMat.mainTexture = portalCamera.targetTexture;
    }

    private void LateUpdate()
    {
        Vector3 playerOffset = playerCamera.position - otherPortal.position;
        transform.position = portal.position + playerOffset;

        float portalRotation = Quaternion.Angle(portal.rotation, otherPortal.rotation);
        Vector3 cameraDirection = Quaternion.AngleAxis(portalRotation, Vector3.up) * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(cameraDirection, Vector3.up);
    }
}

