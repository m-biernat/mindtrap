using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Fade.instance.FadeOut(() => GameState.LoadNextLevel());
        }
    }
}
