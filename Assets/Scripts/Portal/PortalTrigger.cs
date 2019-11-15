using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    public PortalTeleport teleportComponent;

    public byte colliderIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            teleportComponent.PlayerOverlap(colliderIndex, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            teleportComponent.PlayerOverlap(colliderIndex, false);
        }
    }
}
