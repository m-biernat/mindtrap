using UnityEngine;

public class PressAnyKey : MonoBehaviour
{
    public MenuUI menu;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            MenuUI.freshStart = false;
            menu.ChangeView(menu.defaultView);
        }
    }
}
