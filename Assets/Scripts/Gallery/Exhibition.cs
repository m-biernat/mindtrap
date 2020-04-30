using UnityEngine;
using System.Collections.Generic;

public class Exhibition : MonoBehaviour
{
    public List<GameObject> objects;
    public List<Transform> objectPlacements;

    [Space]
    public List<InteractableSocket> sockets;

    private List<InteractableObject> spawnedObjects;
    private int placementsCount;

    public Camera easelCamera;

    [Space]
    public Door door;

    public Exhibition nextExhibition;
    public Transform spawnPoint;

    [Space]
    public Transform effect;

    void Start()
    {
        spawnedObjects = new List<InteractableObject>();

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

        placementsCount = 0;
        List<InteractableObject> selectedObjects = new List<InteractableObject>();

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
        if (value)
        {
            placementsCount++;

            if (placementsCount == sockets.Count)
            {
                door.teleportAction = ChangeExhibition;
                door.gameObject.SetActive(true);

                foreach (var spawnedObject in spawnedObjects)
                {
                    spawnedObject.transform.tag = "Untagged";
                }
            }
        }
        else
        {
            foreach (var socket in sockets)
            {
                if (socket.placedObject)
                {
                    socket.GetComponent<Collider>().enabled = true;
                    socket.placedObject.GetComponent<InteractableObject>().Destroy(1.5f);
                    socket.placedObject = null;
                }
            }

            placementsCount = 0;
        }
    }

    public void ChangeExhibition()
    {
        nextExhibition.gameObject.SetActive(true);
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        player.transform.position = nextExhibition.spawnPoint.position;
        player.transform.rotation = nextExhibition.spawnPoint.rotation;

        gameObject.SetActive(false);

        if (effect)
            effect.position = nextExhibition.transform.position;

        Fade.instance.FadeIn();

        player.GetComponent<PlayerController>().controllLock = false;
    }
}
