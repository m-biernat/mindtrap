using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class InteractionController : MonoBehaviour
{
    private enum Interaction {
        none,
        pickUp,
        place,
        drop
    };

    private Interaction interaction;
    private Interaction prevInteraction;

    private Transform camTransform;

    private RaycastHit hit;

    private Action action;

    private GameObject pickedObject;


    public List<string> instructions;

    private GameObject instructionUI;

    private Text instructionText;
    private Animator instructionAnimator;

    private AudioSource audioSource;

    private void Start()
    {
        camTransform = Camera.main.transform;
        StartCoroutine(Setup());
    }

    private void Update()
    {
        if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, 2.5f))
        {
            switch (hit.transform.tag)
            {
                case "Interactable Object":
                    SetInteraction(Interaction.pickUp, hit.transform);
                    break;

                case "Interactable Socket":
                    if (pickedObject)
                        SetInteraction(Interaction.place, hit.transform);
                    break;

                default:
                    SetInteraction(Interaction.none, null);
                    break;
            }
        }
        else
        {
            SetInteraction(Interaction.none, null);
        }

        if (interaction != Interaction.none)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                action.Invoke();
            }
        }
    }

    private IEnumerator Setup()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            instructionUI = GameObject.FindGameObjectWithTag("PlayerUI");

            if (instructionUI) break;
        }

        instructionUI = instructionUI.transform.Find("Instruction").gameObject;
        instructionUI.SetActive(true);

        instructionText = instructionUI.GetComponent<Text>();
        instructionAnimator = instructionUI.GetComponent<Animator>();
        audioSource = instructionUI.GetComponent<AudioSource>();

        SetInstructionText();
    }

    private void SetInteraction(Interaction interaction, Transform hit)
    {
        prevInteraction = this.interaction;

        if (pickedObject && interaction != Interaction.place)
        {
            if (prevInteraction == Interaction.drop)
                return;

            this.interaction = Interaction.drop;

            action = () => Drop();
            
            SetInstructionText();
        }
        else
        {
            if (prevInteraction == interaction)
                return;

            this.interaction = interaction;

            switch (interaction)
            {
                case Interaction.pickUp:
                    action = () => PickUp(hit.gameObject);
                    break;

                case Interaction.place:
                    action = () => Place(hit);
                    break;
            }

            SetInstructionText();
        }
    }

    private void SetInstructionText()
    {
        int index = (int)interaction;

        if (index > 0)
        {
            audioSource.Play();
            instructionText.text = instructions[index].ToUpper();
            instructionAnimator.SetTrigger("bump");
        }
        else
            instructionText.text = "";
    }

    private void PickUp(GameObject go)
    {
        pickedObject = go;

        go.GetComponent<InteractableObject>().PickUp(transform);

        SetInteraction(Interaction.none, null);
    }

    private void Place(Transform transform)
    {
        transform.GetComponent<InteractableSocket>().PlaceObject(pickedObject);
        
        pickedObject = null;

        SetInteraction(Interaction.none, null);
    }

    private void Drop()
    {
        pickedObject.GetComponent<InteractableObject>().Destroy();
        
        pickedObject = null;

        SetInteraction(Interaction.none, null);
    }
}
