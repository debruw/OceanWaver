using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Target : MonoBehaviour
{
    public enum TargetType
    {
        SandCastle,
        Fire
    }
    MeshRenderer myRenderer;
    ParticleSystem[] particleSystems;

    [SerializeField]
    TargetType myType;

    private void Start()
    {
        if (myType.Equals(TargetType.SandCastle))
        {
            myRenderer = GetComponent<MeshRenderer>();
        }
        else
        {
            particleSystems = GetComponentsInChildren<ParticleSystem>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point") && !GameManager.Instance.isGameOver)
        {
            if (myType.Equals(TargetType.SandCastle))
            {
                StartCoroutine(WaitAndDestroySandCastle(other.transform.parent.gameObject));
            }
            else
            {
                StartCoroutine(WaitAndDestroyFire(other.transform.parent.gameObject));
            }
        }
    }

    IEnumerator WaitAndDestroySandCastle(GameObject go)
    {
        yield return new WaitForSeconds(.25f);
        Destroy(go, 2);
        myRenderer.material.DOFloat(1, "_DissolveAmount", 1);
        StartCoroutine(GameManager.Instance.WaitAndGameWin());
    }

    IEnumerator WaitAndDestroyFire(GameObject go)
    {
        yield return new WaitForSeconds(.1f);
        Destroy(go, 2);
        foreach (var item in particleSystems)
        {
            item.transform.DOScale(Vector3.zero, 1);
        }
        transform.DOMoveY(0, 1);
        StartCoroutine(GameManager.Instance.WaitAndGameWin());
    }
}
