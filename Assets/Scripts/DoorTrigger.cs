using UnityEngine;
using System;

public class DoorTrigger : MonoBehaviour
{
    private float rotation;

    public Action action;

    private void Start()
    {
        rotation = gameObject.transform.parent.rotation.eulerAngles.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && CorrectDirection(other))
        {
            other.GetComponent<PlayerController>().controllLock = true;
            action.Invoke();
        }
    }

    private bool CorrectDirection(Collider other)
    {
        Vector3 dir = (transform.position - other.transform.position).normalized;

        switch (rotation)
        {
            case 0 :
                if (dir.x > 0) return true;
                break;
            case 90:
                if (dir.z < 0) return true;
                break;
            case 180:
                if (dir.x < 0) return true;
                break;
            case 270:
                if (dir.z > 0) return true;
                break;
        }

        return false;
    }
}
