using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public Transform destination;
    private Transform player;

    private bool playerOverlap = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (playerOverlap)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if (dotProduct < 0f)
            {
                float rotation = -Quaternion.Angle(transform.rotation, destination.rotation);
                rotation += 180;
                player.Rotate(Vector3.up, rotation);

                Vector3 positionOffset = Quaternion.Euler(0f, rotation, 0f) * portalToPlayer;
                player.position = destination.position + positionOffset;

                playerOverlap = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerOverlap = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerOverlap = false;
        }
    }
}
