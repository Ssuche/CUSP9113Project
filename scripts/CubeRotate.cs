using UnityEngine;

public class CubeRotate: MonoBehaviour
{
    public float gazeTime = 2.0f;
    public LayerMask interactableLayer;
    public Transform leftButton;
    public Transform rightButton;
    public Transform upButton;
    public float rotationStep = 100f;

    private float gazeTimer = 0.0f;
    private Transform focusedButton;
    private bool isRotating = false;
    private Vector3 rotationAxis = Vector3.zero;

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
            RotateObject(rotationStep * Time.deltaTime);
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
                rotationAxis = Vector3.up;
                rotationStep = -Mathf.Abs(rotationStep);
            }
            else if (button == rightButton)
            {
                rotationAxis = Vector3.up;
                rotationStep = Mathf.Abs(rotationStep);
            }
            else if (button == upButton)
            {
                rotationAxis = Vector3.right;
                rotationStep = Mathf.Abs(rotationStep);
            }

            isRotating = true;
        }
    }

    void RotateObject(float angle)
    {
        transform.RotateAround(transform.position, rotationAxis, angle);
    }

    bool IsButton(Transform obj)
    {
        return obj == leftButton || obj == rightButton || obj == upButton;
    }

    void ResetGaze()
    {
        focusedButton = null;
        gazeTimer = 0.0f;
        isRotating = false;
        rotationAxis = Vector3.zero;
    }
}
