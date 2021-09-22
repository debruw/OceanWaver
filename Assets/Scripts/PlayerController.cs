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
    bool isMouseButtonDown;
    float countDown, maxCountDown = 1f;

    private void Start()
    {
        countDown = maxCountDown;        
    }

    private void Update()
    {
        if (!GameManager.Instance.isGameStarted || GameManager.Instance.isGameOver)
        {
            return;
        }
        countDown += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && countDown > maxCountDown)
        {
            if (GameManager.Instance.currentLevel == 1)
            {
                GameManager.Instance.Tutorial1.SetActive(false);
            }
            if (IsPointerOverUIObject())
            {
                return;
            }
            lineRenderer.enabled = true;
            isMouseButtonDown = true;
            startPosition = Input.mousePosition;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Does the ray intersect any objects excluding the layer
            if (Physics.Raycast(ray, out hit))
            {
                lineRenderer.SetPosition(0, new Vector3(hit.point.x, 0, hit.point.z));
            }
        }
        else if (Input.GetMouseButton(0) && countDown > maxCountDown)
        {
            if (IsPointerOverUIObject())
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
            if (IsPointerOverUIObject())
            {
                return;
            }
            if (isMouseButtonDown && countDown > maxCountDown)
            {
                countDown = 0;
                endPosition = Input.mousePosition;

                ray = Camera.main.ScreenPointToRay(startPosition);
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 newOne = lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0);
                    newOne = newOne / Vector3.Distance(lineRenderer.GetPosition(1), lineRenderer.GetPosition(0));
                    newOne = lineRenderer.GetPosition(0) - (newOne * 8);

                    GameManager.Instance.CreateWave(transform, new Vector3(newOne.x, .94f, newOne.z), -(Mathf.Atan2(lineRenderer.GetPosition(1).z - lineRenderer.GetPosition(0).z, lineRenderer.GetPosition(1).x - lineRenderer.GetPosition(0).x) * 180f / Mathf.PI), Vector3.one);
                }
                lineRenderer.enabled = false;
                isMouseButtonDown = false;
            }
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
