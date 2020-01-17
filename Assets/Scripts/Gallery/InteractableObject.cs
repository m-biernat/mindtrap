using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [HideInInspector] public Transform placement;

    public void Relocate()
    {
        transform.position = placement.position;
        transform.rotation = placement.rotation;
    }

    public void Destroy()
    {
        GetComponent<Collider>().enabled = true;
        
        transform.tag = "Interactable Object";
        transform.parent = placement.parent; // I'll have to change that later
        
        Relocate();
    }
}
