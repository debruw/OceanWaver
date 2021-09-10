using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Reflector : MonoBehaviour
{
    public GameObject particlePrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
        {
            Instantiate(particlePrefab, other.transform.position, Quaternion.identity);
            //GameManager.Instance.CreateWave(GameManager.Instance.playerController.transform, new Vector3(transform.position.x, .94f, transform.position.z), Vector3.Angle(Vector3.Reflect(other.transform.right, Vector3.forward), -Vector3.right), Vector3.one);
            other.transform.parent.DORotate(new Vector3(0, Vector3.Angle(Vector3.Reflect(other.transform.right, Vector3.forward), -Vector3.right), 0), 1f).OnComplete(() =>
              {

              });

            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
