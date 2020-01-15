﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TutorialController : MonoBehaviour
{
    public enum Stage { 
        unset,
        mouseLook, 
        keyboard,
        jump,
        sprintJump,
        faceObject,
        interact,
        desintegrate,
        nextLevel,
    };

    private Stage stage;

    public List<string> instructions;

    private GameObject instructionUI;

    private Text instructionText;
    //private Animator instructionAnimator;

    private bool[] completed = { false, false };

    private float mouseInput = 0.0f;
    private float keyboardInput = 0.0f;

    public GameObject activatedObject;

    public GameObject door;
    public TutorialTrigger nextLevelTrigger;

    private Transform camTransform;

    private void Start()
    {
        StartCoroutine(Setup());
    }

    private void Update()
    {
        if (stage == Stage.mouseLook || stage == Stage.keyboard)
        {
            if (!completed[0])
            {
                mouseInput += Mathf.Abs(Input.GetAxisRaw("Mouse X"));
                mouseInput += Mathf.Abs(Input.GetAxisRaw("Mouse Y"));

                if (mouseInput > 15.0f)
                {
                    completed[0] = true;

                    if (!completed[1])
                        SetStage(Stage.keyboard);
                    else
                        EndStage();
                }
            }

            if (!completed[1])
            {
                keyboardInput += Mathf.Abs(Input.GetAxisRaw("Horizontal"));
                keyboardInput += Mathf.Abs(Input.GetAxisRaw("Vertical"));

                if (keyboardInput > 50.0f)
                {
                    completed[1] = true;
                    if (completed[0]) EndStage();
                }
            }
        }

        else if (stage == Stage.faceObject || stage == Stage.interact)
        {
            RaycastHit hit;

            if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, 1.5f))
            {
                if (hit.transform.tag == "Interactable")
                {
                    SetStage(Stage.interact);
                }
                else
                {
                    SetStage(Stage.faceObject);
                }
            }
            else
            {
                SetStage(Stage.faceObject);
            }

            if (stage == Stage.interact)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    EndStage(Stage.desintegrate);
                }
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
        camTransform = Camera.main.transform;
        
        SetStage(Stage.mouseLook);
    }

    public void SetStage(Stage stage)
    {
        if (this.stage != stage)
        {
            this.stage = stage;
            instructionText.text = instructions[(int)stage].ToUpper();
            InvokeAction();
            //instructionAnimator.SetTrigger("fadeIn");
        }
    }

    public void EndStage(Stage endWithStage = Stage.unset)
    {
        stage = endWithStage;
        instructionText.text = "";
        InvokeAction();
        //instructionAnimator.SetTrigger("fadeOut");
    }

    private void InvokeAction()
    {
        switch(stage)
        {
            case Stage.desintegrate:
                StartCoroutine(Desintegrate());
                break;

            case Stage.nextLevel:
                door.SetActive(true);
                nextLevelTrigger.stage = Stage.nextLevel;
                break;
        }
    }

    private IEnumerator Desintegrate()
    {
        activatedObject.GetComponent<BoxCollider>().enabled = false;

        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.01f);
        }

        activatedObject.SetActive(false);

        SetStage(Stage.nextLevel);
    }
}
