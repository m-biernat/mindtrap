using UnityEngine;
using System.Collections;

public class InteractableObject : MonoBehaviour
{
    [HideInInspector] public Transform placement;

    [HideInInspector] public Vector3 position;
    [HideInInspector] public Vector3 rotation;

    private Renderer m_renderer;

    public void Init()
    {
        position = transform.localPosition;
        rotation = transform.localEulerAngles;

        m_renderer = GetComponent<Renderer>();

        Relocate(placement);
    }

    public void Relocate(Transform target)
    {
        transform.position = target.position + position;
        transform.eulerAngles = target.eulerAngles + rotation;
    }

    public void PickUp(Transform playerTransform)
    {
        transform.SetParent(playerTransform);
        
        transform.localPosition = (Vector3.forward * 1.5f);
        transform.localEulerAngles = rotation;

        GetComponent<Collider>().enabled = false;
    }

    public void Place(Transform socketPlacement)
    {
        transform.parent = placement.parent; // I'll have to change that later

        Relocate(socketPlacement);

        GetComponent<Collider>().enabled = true;

        transform.tag = "Untagged";
    }

    public void Destroy(int delay = 0)
    {
        StartCoroutine(Dissolve(delay));
    }

    private IEnumerator Dissolve(int delay)
    {
        for (int i = 0; i < delay * 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < 50; i++)
        {
            foreach (var material in m_renderer.materials)
            {
                material.SetFloat("_Slider", i / 50.0f);
            }

            yield return new WaitForSeconds(0.01f);
        }

        GetComponent<Collider>().enabled = true;

        transform.tag = "Interactable Object";
        transform.parent = placement.parent; // I'll have to change that later

        Relocate(placement);

        for (int i = 50; i > 0; i--)
        {
            foreach (var material in m_renderer.materials)
            {
                material.SetFloat("_Slider", i / 50.0f);
            }

            yield return new WaitForSeconds(0.01f);
        }
    }
}
