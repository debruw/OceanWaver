using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Reflector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
        {
            GameManager.Instance.CreateWave(GameManager.Instance.playerController.transform, new Vector3(transform.position.x, .94f, transform.position.z), Vector3.Angle(Vector3.Reflect(other.transform.right, Vector3.forward), -Vector3.right), 4, Vector3.one);
            other.transform.parent.DOMoveY(0, .1f).OnComplete(() =>
             {
                 Destroy(other.transform.parent.gameObject);
             });

            gameObject.GetComponent<Collider>().enabled = false;
            StartCoroutine(GameManager.Instance.WaitAndControlNullAttractorShapes(gameObject));
        }
    }
}
