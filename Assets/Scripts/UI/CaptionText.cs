using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Caption Text", menuName = "Caption Text", order = 1)]
public class CaptionText : ScriptableObject
{
    public List<string> defaultInput;
    public List<string> gamepadInput;

    public static bool gamepadInputActive = false;

    public string GetText(int index)
    {
        if (gamepadInputActive)
            return gamepadInput[index];
        else
            return defaultInput[index];
    }
}
