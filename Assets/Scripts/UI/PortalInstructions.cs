using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PortalInstructions : MonoBehaviour
{
    public CaptionText instructions;

    private GameObject instructionUI;

    private Text instructionText;
    private Animator instructionAnimator;

    private AudioSource audioSource;

    private void Start()
    {
        StartCoroutine(Setup());
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

        instructionText.text = "";

        yield return new WaitForSeconds(0.5f);

        audioSource.Play();
        instructionText.text = instructions.GetText(0).ToUpper();
        instructionAnimator.SetTrigger("bump");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            instructionUI.SetActive(false);
            enabled = false;
        }
    }
}
