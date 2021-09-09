using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    Vector3 startPosition, endPosition, direction;
    float distance;
    public LineRenderer lineRenderer;
    RaycastHit hit;
    Ray ray;
    public bool isOverUI;
    bool isMouseButtonDown;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            isMouseButtonDown = true;
            startPosition = Input.mousePosition;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(ray, out hit))
            {
                lineRenderer.SetPosition(0, new Vector3(hit.point.x, 0, hit.point.z));
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            direction = new Vector3(endPosition.x, 0, endPosition.y) - new Vector3(startPosition.x, 0, startPosition.y);
            distance = direction.magnitude / 10;
            //lineRenderer.SetPosition(1, direction.normalized * distance);

            endPosition = Input.mousePosition;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(ray, out hit))
            {
                lineRenderer.SetPosition(1, new Vector3(hit.point.x, 0, hit.point.z));
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            if (isMouseButtonDown)
            {
                endPosition = Input.mousePosition;

                ray = Camera.main.ScreenPointToRay(startPosition);
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log(hit.point);
                    if (startPosition.x > Input.mousePosition.x)
                    {
                        GameManager.Instance.CreateWave(transform, new Vector3(hit.point.x + 3, .94f, transform.position.z - 3), -(Mathf.Atan2(lineRenderer.GetPosition(1).z - lineRenderer.GetPosition(0).z, lineRenderer.GetPosition(1).x - lineRenderer.GetPosition(0).x) * 180 / Mathf.PI) + 180, 8, Vector3.one);
                    }
                    else
                    {
                        GameManager.Instance.CreateWave(transform, new Vector3(hit.point.x - 3, .94f, transform.position.z - 3), -(Mathf.Atan2(lineRenderer.GetPosition(1).z - lineRenderer.GetPosition(0).z, lineRenderer.GetPosition(1).x - lineRenderer.GetPosition(0).x) * 180 / Mathf.PI) + 180, 8, Vector3.one);
                    }
                }
                isMouseButtonDown = false;
            }            
        }
    }
}
