using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ITemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    Vector3 firstPos;
    public GameObject ReflectorPrefab;
    GameObject instantiated;

    private void Start()
    {
        firstPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        RectTransform invPanel = transform.parent.transform as RectTransform;

        if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
        {
            if (instantiated == null)
            {
                gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(ray, out hit))
                {
                    instantiated = Instantiate(ReflectorPrefab, new Vector3(hit.point.x, 0, hit.point.z), Quaternion.identity);
                }

            }
        }
        if (instantiated != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Does the ray intersect any objects excluding the player layer
            Physics.Raycast(ray, out hit);
            instantiated.transform.position = new Vector3(hit.point.x, 0, hit.point.z);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = firstPos;
        gameObject.GetComponent<Image>().color = Color.white;
        instantiated = null;
    }
}
