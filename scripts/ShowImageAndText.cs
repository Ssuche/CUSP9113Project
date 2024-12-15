using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShowImageAndDesc : MonoBehaviour
{
    public LayerMask interactableLayer;
    public Image displayImage;
    public GameObject desc;
    public float displayDuration = 5.0f;

    private bool isDisplaying = false;
    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactableLayer))
        {
            if (hit.transform == transform && !isDisplaying)
            {
                StartCoroutine(DisplayContent());
            }
        }
    }

    IEnumerator DisplayContent()
    {
        isDisplaying = true;

        if (displayImage != null) displayImage.gameObject.SetActive(true);
        if (desc != null) desc.SetActive(true);

        yield return new WaitForSeconds(displayDuration);

        if (displayImage != null) displayImage.gameObject.SetActive(false);
        if (desc != null) desc.SetActive(false);

        isDisplaying = false;
    }
}
