using UnityEngine;
using System.Collections.Generic;

public class Wave : MonoBehaviour
{
    public GameObject prefab;

    [Header("Starting parameters")]
    public int edgeCount = 10;
    public float yRotation = 0.0f;
    public float spacing = 2.5f;
    public bool rounded = false;

    private int m_edgeCount; 
    private GameObject[,] objects;

    [Header("Wave parameters")]
    public float amplitude = 0.5f;
    public float strength = 0.5f;
    public float step = 0.05f;

    private float offset = 0.0f;

    private void Start()
    {
        m_edgeCount = edgeCount;
        objects = new GameObject[edgeCount, edgeCount];

        GameObject go;
        Vector3 position;

        Vector3 positionOffset = transform.position;
        float size = ((edgeCount - 1) * spacing) / 2.0f;
        positionOffset.x -= size;
        positionOffset.z += size;

        for (int i = 0; i < edgeCount; i++)
        {
            for (int j = 0; j < edgeCount; j++)
            {
                position = new Vector3(i * spacing, 0.0f, -j * spacing);
                position += positionOffset;

                if (rounded)
                {
                    float dist = Vector3.Distance(position, transform.position);
                    float scale = Mathf.Clamp((size - dist) / size, 0.0f, 1.0f);
                    
                    if (scale > 0.0f)
                    {
                        go = Instantiate(prefab, position, prefab.transform.rotation, transform);
                        go.transform.localScale *= scale;
                    }  
                    else
                        go = null;
                }
                else
                    go = Instantiate(prefab, position, prefab.transform.rotation, transform);

                objects[i, j] = go;
            }
        }

        transform.rotation = Quaternion.Euler(transform.rotation.x, yRotation, transform.rotation.z);
    }


    private void LateUpdate()
    {
        for (int i = 0; i < m_edgeCount; i++)
        {
            for (int j = 0; j < m_edgeCount; j++)
            {
                if (objects[i, j])
                {
                    Vector3 position = objects[i, j].transform.localPosition;

                    position.y = Mathf.Sin((i + j) * amplitude + offset) * strength;

                    objects[i, j].transform.localPosition = position;
                }
            }
        }

        offset += step;
        if (offset > 6.28f)
            offset = 0.0f;
    }
}

