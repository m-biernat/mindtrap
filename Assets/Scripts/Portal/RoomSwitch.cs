using UnityEngine;

public class RoomSwitch : MonoBehaviour
{
    public RoomConfig roomConfig;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            roomConfig.prevRoom.SetActive(false); // or Destroy
            roomConfig.nextRoom.SetActive(true);

            gameObject.SetActive(false);
        }
    }
}
