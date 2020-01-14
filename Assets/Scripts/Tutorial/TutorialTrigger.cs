using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public TutorialController tutorialController;

    public TutorialController.Stage stage;

    public bool executeOnEnter, executeOnExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && executeOnEnter)
        {
            tutorialController.SetStage(stage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && executeOnExit)
        {
            tutorialController.EndStage();
        }
    }
}
