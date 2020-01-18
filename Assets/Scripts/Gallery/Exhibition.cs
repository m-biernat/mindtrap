using UnityEngine;
using System.Collections.Generic;

public class Exhibition : MonoBehaviour
{
    public List<GameObject> objects;
    public List<Transform> objectPlacements;

    [Space]
    public List<InteractableSocket> sockets;

    private List<bool> correctPlacements;

    public Camera easelCamera;

    [Space]
    public Door door;

    public Exhibition nextExhibition;
    public Transform spawnPoint;

    void Start()
    {
        List<InteractableObject> selectedObjects = new List<InteractableObject>();
        List<InteractableObject> spawnedObjects = new List<InteractableObject>();

        foreach (var placement in objectPlacements)
        {
            int index = Random.Range(0, objects.Count);

            GameObject go = Instantiate(objects[index], transform);

            go.tag = "Interactable Object";
            go.AddComponent(typeof(InteractableObject));

            InteractableObject interactableObject = go.GetComponent<InteractableObject>();
            interactableObject.placement = placement;

            interactableObject.Init();

            spawnedObjects.Add(interactableObject);

            objects.RemoveAt(index);
        }

        correctPlacements = new List<bool>();

        for (int i = 0; i < sockets.Count; i++)
        {
            sockets[i].exhibition = this;
            sockets[i].transform.tag = "Interactable Socket";

            int index = Random.Range(0, spawnedObjects.Count);
      
            selectedObjects.Add(spawnedObjects[index]);

            if (sockets[i].placement == null)
                sockets[i].placement = sockets[i].transform;

            spawnedObjects[index].Relocate(sockets[i].placement);

            sockets[i].fittingObject = spawnedObjects[index].gameObject;

            spawnedObjects.RemoveAt(index);
        }

        easelCamera.Render();
        
        foreach(var selectedObject in selectedObjects)
        {
            selectedObject.Relocate(selectedObject.placement);
        }
    }

    public void Proceed(bool value)
    {
        correctPlacements.Add(value);

        if (correctPlacements.Count == sockets.Count)
        {
            bool allCorrect = true;

            foreach (var placement in correctPlacements)
            {
                if (placement == false)
                    allCorrect = false;
            }

            if (allCorrect)
            {
                door.teleportAction = ChangeExhibition;
                door.gameObject.SetActive(true);
            }
            else
            {
                foreach (var socket in sockets)
                {
                    socket.placedObject.GetComponent<InteractableObject>().Destroy();
                    socket.placedObject = null;
                }

                correctPlacements.Clear();
            }
        }
    }

    public void ChangeExhibition()
    {
        nextExhibition.gameObject.SetActive(true);
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        player.transform.position = nextExhibition.spawnPoint.position;
        player.transform.rotation = nextExhibition.spawnPoint.rotation;

        gameObject.SetActive(false);

        Fade.instance.FadeIn();

        player.GetComponent<PlayerController>().controllLock = false;
    }
}
