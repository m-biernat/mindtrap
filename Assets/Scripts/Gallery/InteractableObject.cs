using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [HideInInspector] public Transform placement;

    public void Relocate()
    {
        transform.position = placement.position;
        transform.rotation = placement.rotation;
    }

    public void Reset()
    {

    }
}
