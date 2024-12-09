using UnityEngine;

public class CubeRotate : MonoBehaviour
{
    public float gazeTime = 2.0f; 
    public LayerMask interactableLayer; 
    public Transform leftButton; 
    public Transform rightButton; 
    public float rotationStep = 20f; 
    public Vector3 worldRotationPoint = Vector3.zero; 

    private float gazeTimer = 0.0f; 
    private Transform focusedButton; 
    private bool isRotating = false; 
    private float rotationDirection = 0f; 

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactableLayer) && IsButton(hit.transform))
        {
            HandleButtonFocus(hit.transform);
        }
        else
        {
            ResetGaze(); 
        }

        if (isRotating)
        {
            RotateObject(rotationDirection * rotationStep * Time.deltaTime);
        }
    }

    void HandleButtonFocus(Transform button)
    {
        if (focusedButton == null || focusedButton != button)
        {
            focusedButton = button;
            gazeTimer = 0.0f;
        }

        gazeTimer += Time.deltaTime;

        if (gazeTimer >= gazeTime)
        {
            if (button == leftButton)
            {
                rotationDirection = -1f;
            }
            else if (button == rightButton)
            {
                rotationDirection = 1f; 
            }

            isRotating = true; 
        }
    }

    void RotateObject(float angle)
    {
        transform.RotateAround(worldRotationPoint, Vector3.up, angle);
    }

    bool IsButton(Transform obj)
    {
        return obj == leftButton || obj == rightButton;
    }

    void ResetGaze()
    {
        focusedButton = null;
        gazeTimer = 0.0f;
        isRotating = false; 
        rotationDirection = 0f; 
    }
}
