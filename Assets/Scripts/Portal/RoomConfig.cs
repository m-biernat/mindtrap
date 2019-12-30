using UnityEngine;
using System.Collections.Generic;

public class RoomConfig : MonoBehaviour
{
    public GameObject prevRoom, nextRoom;

    [Space]
    public List<GameObject> disabledObjects;

    public void SwitchActive()
    {
        prevRoom.SetActive(false); // or Destroy
        nextRoom.SetActive(true);
    }

    public void DisableObjects()
    {
        if (disabledObjects != null)
        {
            foreach(var obj in disabledObjects)
            {
                obj.SetActive(false);
            }
        }
    }

    public void EnableObjects()
    {
        if (disabledObjects != null)
        {
            foreach (var obj in disabledObjects)
            {
                obj.SetActive(true);
            }
        }
    }
}
