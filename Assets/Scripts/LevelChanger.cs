using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameState.LoadNextLevel();
        }
    }
}
