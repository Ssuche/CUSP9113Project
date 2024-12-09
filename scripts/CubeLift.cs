using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CubeLift : MonoBehaviour
{
    public float gazeTime = 3.0f; 
    public LayerMask interactableLayer; 
    public float liftAmount = 0.5f; 
    public Image progressCircle; 
    public GameObject leftButton;
    public GameObject rightButton; 
    public GameObject upButton; 
    private float gazeTimer = 0.0f; 
    private Vector3 originalPosition; 
    private bool isLifted = false; 

    void Start()
    {
        originalPosition = transform.position;
        ToggleButtons(false);
    }

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactableLayer) && hit.transform == transform)
        {
            gazeTimer += Time.deltaTime;
            progressCircle.fillAmount = gazeTimer / gazeTime;

            if (gazeTimer >= gazeTime)
            {
                if (isLifted)
                {
                    LowerCube();
                }
                else
                {
                    LiftCube();
                }
                ResetGaze();
            }
        }
        else
        {
            ResetGaze();
        }
    }

    void LiftCube()
    {
        Vector3 targetPosition = transform.position + new Vector3(0, liftAmount, 0);
        StartCoroutine(SmoothMove(transform.position, targetPosition, 1.0f));
        isLifted = true;

        ToggleButtons(true);
        GetComponent<ButtonPositionController>()?.ToggleButtons(true);

    }

    void LowerCube()
    {
        StartCoroutine(SmoothMove(transform.position, originalPosition, 1.0f));
        isLifted = false;
        GetComponent<ButtonPositionController>()?.ToggleButtons(false);
    }

    IEnumerator SmoothMove(Vector3 start, Vector3 end, float duration)
    {
        float elapsed = 0;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = end;
    }

    void ToggleButtons(bool isVisible)
    {
        if (leftButton != null)
            leftButton.SetActive(isVisible);
        if (rightButton != null)
            rightButton.SetActive(isVisible);
        if (upButton != null)
            upButton.SetActive(isVisible);
    }

    void ResetGaze()
    {
        gazeTimer = 0.0f;
        progressCircle.fillAmount = 0;
    }
}
