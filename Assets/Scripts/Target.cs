using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Target : MonoBehaviour
{
    MeshRenderer myRenderer;

    private void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point") && !GameManager.Instance.isGameOver)
        {
            StartCoroutine(WaitAndDestroy(other.transform.parent.gameObject));
        }
    }

    IEnumerator WaitAndDestroy(GameObject go)
    {
        yield return new WaitForSeconds(.25f);
        Destroy(go, 2);
        myRenderer.material.DOFloat(1, "_DissolveAmount", 1);
        StartCoroutine(GameManager.Instance.WaitAndGameWin());
    }
}
