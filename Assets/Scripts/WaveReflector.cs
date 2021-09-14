using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaveReflector : MonoBehaviour
{
    public GameObject particlePrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
        {
            Instantiate(particlePrefab, other.transform.position, Quaternion.identity);
            other.transform.parent.DORotate(new Vector3(0, Vector3.Angle(Vector3.Reflect(other.transform.right, Vector3.forward), -Vector3.right), 0), .25f).OnComplete(() =>
              {

              });

            gameObject.GetComponent<Collider>().enabled = false;
            StartCoroutine(WaitAndOpenCollider());
        }
    }

    IEnumerator WaitAndOpenCollider()
    {
        yield return new WaitForSeconds(.5f);
        gameObject.GetComponent<Collider>().enabled = true;
    }
}
