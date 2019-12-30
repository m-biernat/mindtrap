using UnityEngine;

public class RoomSwitch : MonoBehaviour
{
    public RoomConfig roomConfig;

    private void Start()
    {
        roomConfig.DisableObjects();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            roomConfig.EnableObjects();
            roomConfig.SwitchActive();

            gameObject.SetActive(false);
        }
    }
}
