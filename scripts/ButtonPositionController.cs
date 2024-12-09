using UnityEngine;

public class ButtonPositionController : MonoBehaviour
{
    public GameObject leftButton; 
    public GameObject rightButton; 
    public GameObject upButton;
    public float buttonDistance = 1.0f;
    public float buttonHeightOffset = 0.5f;
    public float buttonHeightOffsetup = 1f;

    void Start()
    {

        ToggleButtons(false);
    }

    public void ToggleButtons(bool isVisible)
    {
        if (leftButton != null)
            leftButton.SetActive(isVisible);
        if (rightButton != null)
            rightButton.SetActive(isVisible);
        if (upButton != null)
            upButton.SetActive(isVisible);
        if (isVisible)
        {
            UpdateButtonPositions();
        }
    }

    void UpdateButtonPositions()
    {
        if (Camera.main == null) return;

        Vector3 cubePosition = transform.position;
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 directionToCamera = (cameraPosition - cubePosition).normalized;

        Vector3 leftOffset = Vector3.Cross(directionToCamera, Vector3.up).normalized * buttonDistance;
        Vector3 rightOffset = -leftOffset;
        Vector3 upOffset = Vector3.up * buttonHeightOffset * 2;

        Vector3 leftButtonPosition = cubePosition + leftOffset + Vector3.up * buttonHeightOffset;
        Vector3 rightButtonPosition = cubePosition + rightOffset + Vector3.up * buttonHeightOffset;
        Vector3 upButtonPosition = cubePosition + upOffset+ Vector3.up * buttonHeightOffsetup;

        if (leftButton != null)
            leftButton.transform.position = leftButtonPosition;
        if (rightButton != null)
            rightButton.transform.position = rightButtonPosition;
        if (upButton != null)
            upButton.transform.position = upButtonPosition;

        FaceCamera(leftButton);
        FaceCamera(rightButton);
        FaceCamera(upButton);
    }

    void FaceCamera(GameObject button)
    {
        if (button != null && Camera.main != null)
        {
            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 direction = cameraPosition - button.transform.position;
        }
    }
}
