using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInteraction : MonoBehaviour
{
    private Renderer cubeRenderer;
    private Color originalColor;
    public Color gazeColor = Color.red;
    // Start is called before the first frame update
    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        originalColor = cubeRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform)
            {
                cubeRenderer.material.color = gazeColor; 
            }
            else
            {
                cubeRenderer.material.color = originalColor; 
            }
        }
        else
        {
            cubeRenderer.material.color = originalColor; 
        }
    }
}
