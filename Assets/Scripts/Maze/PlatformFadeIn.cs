using UnityEngine;

public class PlatformFadeIn : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    [Space]
    public Material mainMaterial;
    public Material fadeMaterial;
    [Space]
    public float speed = 1.0f;

    private bool fadeIn = false;
    private float fadeValue;
    private float t = 0.0f;

    private void Start()
    {
        meshRenderer.material = fadeMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            fadeIn = true;   
        }
    }

    private void Update()
    {
        if (fadeIn)
        {
            fadeValue = Mathf.Lerp(0.0f, 1.0f, t);
            meshRenderer.material.SetFloat("Vector1_7B8C7FBB", fadeValue);

            t += speed * Time.deltaTime;

            if (fadeValue >= 1.0f)
            {
                meshRenderer.material = mainMaterial;
                Destroy(gameObject);
            }
        }
    }
}
