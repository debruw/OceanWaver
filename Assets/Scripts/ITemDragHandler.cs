using System.Collections;
using System.Collections.Generic;
using TapticPlugin;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ITemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    Vector3 firstPos;
    public GameObject ReflectorPrefab;
    GameObject instantiated;
    public TextMeshProUGUI countText;
    public int count = 2;

    private void Start()
    {
        firstPos = transform.position;
        countText.text = count.ToString();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (count <= 0)
        {
            return;
        }
        if (GameManager.Instance.currentLevel == 2)
        {
            GameManager.Instance.Tutorial2.SetActive(false);
        }
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
        if (count <= 0)
        {
            return;
        }
        count--;
        countText.text = count.ToString();
        if (GameManager.Instance.currentLevel == 2)
        {
            GameManager.Instance.Tutorial3.SetActive(true);
        }
        transform.position = firstPos;
        gameObject.GetComponent<Image>().color = Color.white;
        instantiated = null;
        SoundManager.Instance.playSound(SoundManager.GameSounds.ReflectorPlace);
        if (PlayerPrefs.GetInt("VIBRATION") == 1)
            TapticManager.Impact(ImpactFeedback.Light);
    }
}
