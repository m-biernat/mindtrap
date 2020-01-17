using UnityEngine;
using System.Collections.Generic;

public class Exhibition : MonoBehaviour
{
    public List<GameObject> objects;
    public List<Transform> objectPlacements;

    [Space]
    public List<InteractableSocket> sockets;
    
    private List<InteractableObject> selectedObjects;
    private List<bool> correctPlacements;

    public Camera easelCamera;

    void Start()
    {
        List<InteractableObject> spawnedObjects = new List<InteractableObject>();

        foreach (var placement in objectPlacements)
        {
            int index = Random.Range(0, objects.Count);

            GameObject go = Instantiate(objects[index], transform);

            go.tag = "Interactable Object";
            go.AddComponent(typeof(InteractableObject));

            InteractableObject interactableObject = go.GetComponent<InteractableObject>();
            interactableObject.placement = placement;

            interactableObject.Relocate();

            spawnedObjects.Add(interactableObject);

            objects.RemoveAt(index);
        }

        
        selectedObjects = new List<InteractableObject>();
        correctPlacements = new List<bool>();

        for (int i = 0; i < sockets.Count; i++)
        {
            sockets[i].exhibition = this;

            int index = Random.Range(0, spawnedObjects.Count);
      
            selectedObjects.Add(spawnedObjects[index]);

            if (sockets[i].placement == null)
                sockets[i].placement = sockets[i].transform;

            spawnedObjects[index].transform.position = sockets[i].placement.position;
            spawnedObjects[index].transform.rotation = sockets[i].placement.rotation;

            sockets[i].fittingObject = spawnedObjects[index].gameObject;
            sockets[i].index = i;

            correctPlacements.Add(false);

            spawnedObjects.RemoveAt(index);
        }

        easelCamera.Render();

        
        foreach(var selectedObject in selectedObjects)
        {
            selectedObject.Relocate();
        }
    }

    public void CheckProgress()
    {
        Debug.Log("DUPA");
    }
}
