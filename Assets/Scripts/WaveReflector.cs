using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TapticPlugin;

public class WaveReflector : MonoBehaviour
{
    public GameObject particlePrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
        {
            SoundManager.Instance.playSound(SoundManager.GameSounds.Splash);
            if (PlayerPrefs.GetInt("VIBRATION") == 1)
                TapticManager.Impact(ImpactFeedback.Light);
            Instantiate(particlePrefab, other.transform.position, Quaternion.identity);
            Instantiate(particlePrefab, other.transform.position + new Vector3(0, 0, 1), Quaternion.identity);
            Instantiate(particlePrefab, other.transform.position + new Vector3(0, 0, 2), Quaternion.identity);
            other.transform.parent.DORotate(new Vector3(0, Vector3.Angle(Vector3.Reflect(other.transform.right, Vector3.forward), -Vector3.right), 0), .25f);
            gameObject.GetComponent<Collider>().enabled = false;
            StartCoroutine(WaitAndOpenCollider(1f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Point"))
        {
            StartCoroutine(WaitAndOpenCollider(.5f));
        }
    }

    IEnumerator WaitAndOpenCollider(float count)
    {
        yield return new WaitForSeconds(count);
        gameObject.GetComponent<Collider>().enabled = true;
    }
}
