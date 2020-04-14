using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
    private Stage prevStage;

    public CaptionText instructions;

    private GameObject instructionUI;

    private Text instructionText;
    private Animator instructionAnimator;

    private bool[] completed = { false, false };

    private float mouseInput = 0.0f;
    private float keyboardInput = 0.0f;

    public GameObject interactableObject;

    public GameObject activatedObject;
    public TutorialTrigger nextLevelTrigger;

    private Transform camTransform;

    private AudioSource audioSource;

    private void Start()
    {
        StartCoroutine(Setup());
    }

    private void Update()
    {
        if (stage == Stage.mouseLook || stage == Stage.keyboard)
        {
            if (PlayerUI.pauseMenuActive)
                return;

            if (!completed[0])
            {
                mouseInput += Mathf.Abs(Input.GetAxisRaw("MouseLook X"));
                mouseInput += Mathf.Abs(Input.GetAxisRaw("MouseLook Y"));

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
                keyboardInput += Mathf.Abs(Input.GetAxisRaw("Movement X"));
                keyboardInput += Mathf.Abs(Input.GetAxisRaw("Movement Y"));

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
                if (Input.GetButtonDown("Interact"))
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
        instructionAnimator = instructionUI.GetComponent<Animator>();
        camTransform = Camera.main.transform;

        audioSource = instructionUI.GetComponent<AudioSource>();

        SetStage(Stage.mouseLook);
    }

    public void SetStage(Stage stage)
    {
        if (this.stage != stage)
        {
            prevStage = this.stage;
            this.stage = stage;
            
            audioSource.Play();
            instructionText.text = instructions.GetText((int)stage).ToUpper();
            instructionAnimator.SetTrigger("bump");

            InvokeAction();
        }
    }

    public void EndStage(Stage endWithStage = Stage.unset)
    {
        stage = endWithStage;
        instructionText.text = "";
   
        InvokeAction();
    }

    private void InvokeAction()
    {
        switch(stage)
        {
            case Stage.desintegrate:
                StartCoroutine(Desintegrate());
                break;

            case Stage.nextLevel:
                activatedObject.SetActive(true);
                nextLevelTrigger.stage = Stage.nextLevel;
                break;
        }
    }

    private IEnumerator Desintegrate()
    {
        interactableObject.GetComponent<BoxCollider>().enabled = false;

        interactableObject.GetComponent<AudioSource>().Play();

        Material mat = interactableObject.GetComponent<Renderer>().material;

        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.01f);
            mat.SetFloat("_Slider", i / 100.0f);
        }

        interactableObject.SetActive(false);

        SetStage(Stage.nextLevel);
    }
}
