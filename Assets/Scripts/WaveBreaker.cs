using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaveBreaker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
        {
            ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem item in particles)
            {
                item.Play();
            }
            int pointNo = other.GetComponent<Point>().pointNo;
            //Debug.Log(pointNo);            
            if (pointNo == 3 || pointNo == 4 || pointNo == 5)
            {
                DOTween.Pause(other.transform.parent.transform);
                Collider[] colliders = other.transform.gameObject.GetComponentsInChildren<Collider>();
                foreach (var item in colliders)
                {
                    item.enabled = false;
                }
                other.transform.parent.transform.DOMoveY(0, .1f).OnComplete(() =>
                {
                    Debug.Log("3");
                    Destroy(other.transform.parent.gameObject);
                });
                StartCoroutine(GameManager.Instance.WaitAndControlNullAttractorShapes(gameObject));
            }
            else
            {
                other.GetComponent<Collider>().enabled = false;
                other.GetComponentInParent<MegaShapeCircle>().splines[0].knots[pointNo].MoveKnot(0, -1, 0);
                other.GetComponentInParent<MegaShapeCircle>().AutoCurve();
                //GameManager.Instance.CreateWave(GameManager.Instance.playerController.transform, transform.position, 360 - other.transform.eulerAngles.y, 12, new Vector3(.5f, .5f, .5f));
                GameManager.Instance.CheckDestroyedPoints();
            }
        }
    }
}
