using UnityEngine;

public class InteractableSocket : MonoBehaviour
{
    public Transform placement;

    [HideInInspector] public Exhibition exhibition;
    [HideInInspector] public int index;

    [HideInInspector] public GameObject fittingObject;
    [HideInInspector] public GameObject placedObject;

    public void PlaceObject(GameObject go)
    {
        go.transform.position = placement.position;
        go.transform.rotation = placement.rotation;

        placedObject = go;
        exhibition.CheckProgress();
    }
}
