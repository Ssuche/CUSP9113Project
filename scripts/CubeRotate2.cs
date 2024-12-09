using UnityEngine;

public class CubeRotate2 : MonoBehaviour
{
    public float gazeTime = 2.0f;
    public LayerMask interactableLayer;
    public Transform upButton; 
    public float rotationStep = 20f; 
    public Vector3 worldRotationPoint = Vector3.zero; 

    private float gazeTimer = 0.0f; 
    private Transform focusedButton; 
    private bool isRotating = false; 

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactableLayer) && hit.transform == upButton)
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
            isRotating = true;
        }
    }

    void RotateObject(float angle)
    {
        transform.RotateAround(worldRotationPoint, Vector3.right, angle);
    }

    void ResetGaze()
    {
        focusedButton = null;
        gazeTimer = 0.0f;
        isRotating = false;
    }
}
