using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMove : MonoBehaviour
{
    public LayerMask groundLayer; 
    public float gazeTime = 3.0f; 
    public float moveSpeed = 2.0f; 
    private float gazeTimer = 0.0f; 
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if ((groundLayer.value & (1 << hit.collider.gameObject.layer)) > 0)
            {
                gazeTimer += Time.deltaTime;
                if (gazeTimer >= gazeTime && !isMoving)
                {
                    targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                    isMoving = true; 
                }
            }
            else
            {
                gazeTimer = 0.0f; 
                isMoving = false; 
            }
        }
        else
        {
            gazeTimer = 0.0f;
            isMoving = false; 
        }
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false; 
            gazeTimer = 0.0f; 
        }
    }
}
