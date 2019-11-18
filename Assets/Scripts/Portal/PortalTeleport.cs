using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public Transform origin, destination;
    private Transform player;

    private bool[] playerOverlap = { false, false };
    private byte[] overlapOrder = { 0, 0 };

    private byte orderNumber = 0;

    public Camera targetPortalCamera;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void PlayerOverlap(byte index, bool state)
    {
        playerOverlap[index] = state;

        if (state)
        {
            overlapOrder[index] = ++orderNumber;

            if (playerOverlap[0] == true && playerOverlap[1] == true
                && overlapOrder[0] < overlapOrder[1])
            {
                TeleportPlayer();
                PortalManager.SetActiveCamera(targetPortalCamera);
            }
        }
        else
        {
            if (playerOverlap[0] == false && playerOverlap[1] == false)
            {
                overlapOrder[0] = overlapOrder[1] = orderNumber = 0;
            }
        }
    }

    private void TeleportPlayer()
    {
        Vector3 portalToPlayer = player.position - origin.position;

        float rotation = -Quaternion.Angle(origin.rotation, destination.rotation);
        rotation += 180;
        player.Rotate(Vector3.up, rotation);

        Vector3 positionOffset = Quaternion.Euler(0f, rotation, 0f) * portalToPlayer;
        player.position = destination.position + positionOffset;
    }

}
