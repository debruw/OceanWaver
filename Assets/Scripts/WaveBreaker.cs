using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TapticPlugin;

public class WaveBreaker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
        {
            SoundManager.Instance.playSound(SoundManager.GameSounds.Splash);
            if (PlayerPrefs.GetInt("VIBRATION") == 1)
                TapticManager.Impact(ImpactFeedback.Light);
            ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem item in particles)
            {
                item.Play();
            }
            int pointNo = other.GetComponent<Point>().pointNo;
                      
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
                    Destroy(other.transform.parent.gameObject);
                });
                StartCoroutine(GameManager.Instance.WaitAndControlNullAttractorShapes(gameObject));
            }
            else
            {
                other.GetComponent<Collider>().enabled = false;
                other.GetComponentInParent<MegaShapeCircle>().splines[0].knots[pointNo].MoveKnot(0, -1, 0);
                other.GetComponentInParent<MegaShapeCircle>().AutoCurve();
                GameManager.Instance.CheckDestroyedPoints();
            }
        }
    }
}
