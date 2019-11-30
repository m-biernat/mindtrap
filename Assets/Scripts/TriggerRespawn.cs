using UnityEngine;

public class TriggerRespawn : MonoBehaviour
{
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // FadeOutScreen

            Respawn(other.gameObject);

            // FadeInScreen
        }
    }

    public void Respawn(GameObject player)
    {
        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;
        player.GetComponent<PlayerController>().camRotation = 0.0f;
    }

    public void SetSpawnPoint(Transform spawnPoint)
    {
        this.spawnPoint = spawnPoint;
    }
}
