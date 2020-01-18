using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [HideInInspector] public Transform placement;

    [HideInInspector] public Vector3 position;
    [HideInInspector] public Vector3 rotation;

    public void Init()
    {
        position = transform.localPosition;
        rotation = transform.localEulerAngles;

        Relocate(placement);
    }

    public void Relocate(Transform target)
    {
        transform.position = target.position + position;
        transform.eulerAngles = target.eulerAngles + rotation;
    }

    public void PickUp(Transform playerTransform)
    {
        transform.SetParent(playerTransform);
        
        transform.localPosition = (Vector3.forward * 1.5f);
        transform.localEulerAngles = rotation;

        GetComponent<Collider>().enabled = false;
    }

    public void Place(Transform socketPlacement)
    {
        transform.parent = placement.parent; // I'll have to change that later

        Relocate(socketPlacement);

        GetComponent<Collider>().enabled = true;

        transform.tag = "Untagged";
    }

    public void Destroy()
    {
        GetComponent<Collider>().enabled = true;
        
        transform.tag = "Interactable Object";
        transform.parent = placement.parent; // I'll have to change that later
        
        Relocate(placement);
    }
}
