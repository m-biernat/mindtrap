using UnityEngine;
using System.Collections.Generic;

public class Exhibition : MonoBehaviour
{
    public List<GameObject> objects;
    public List<Transform> objectPlacements;

    [Space]
    public List<Transform> sockets;
    
    private Dictionary<int, GameObject> selectedObjects;

    public Camera easelCamera;

    void Start()
    {
        List<int> indices = new List<int>();
        List<GameObject> spawnedObjects = new List<GameObject>();

        for (int i = 0; i < objectPlacements.Count; i++)
        {
            int index = Random.Range(0, objects.Count);

            GameObject go = Instantiate(objects[index], transform);
            go.transform.position = objectPlacements[i].position;
            go.transform.rotation = objectPlacements[i].rotation;

            indices.Add(i);
            spawnedObjects.Add(go);

            objects.RemoveAt(index);
        }

        selectedObjects = new Dictionary<int, GameObject>();

        foreach (var socket in sockets)
        {
            int index = Random.Range(0, indices.Count);
            int key = indices[index];
            
            selectedObjects.Add(key, spawnedObjects[key]);
            
            selectedObjects[key].transform.position = socket.position;
            selectedObjects[key].transform.rotation = socket.rotation;

            indices.RemoveAt(index);
            spawnedObjects.RemoveAt(index);
        }

        indices = null;
        spawnedObjects = null;

        easelCamera.Render();

        foreach(var selectedObject in selectedObjects)
        {
            selectedObject.Value.transform.position = objectPlacements[selectedObject.Key].position;
            selectedObject.Value.transform.rotation = objectPlacements[selectedObject.Key].rotation;
        }
    }
}
