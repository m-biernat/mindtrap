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

        placedObject.transform.SetParent(exhibition.transform);

        placedObject.transform.position = placement.position;
        placedObject.transform.rotation = placement.rotation;

        placedObject.GetComponent<Collider>().enabled = true;

        placedObject.transform.tag = "Untagged";

        bool value = placedObject == fittingObject ? true : false;
        exhibition.Proceed(value);
    }
}
