using UnityEngine;

public class InteractableSocket : MonoBehaviour
{
    public Transform placement;

    [HideInInspector] public Exhibition exhibition;

    [HideInInspector] public GameObject fittingObject;
    [HideInInspector] public GameObject placedObject;

    public void PlaceObject(GameObject go)
    {
        placedObject = go;

        go.GetComponent<InteractableObject>().Place(placement);

        bool value = placedObject == fittingObject ? true : false;
        exhibition.Proceed(value);
    }
}
