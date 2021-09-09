using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public PlayerController playerController;
    public GameObject Water;
    public GameObject WavePrefab;
    GameObject newWave;

    public void CreateWave(Transform parent, Vector3 position, float yRot, float moveTime, Vector3 size)
    {
        newWave = Instantiate(WavePrefab, position, Quaternion.identity, parent);
        newWave.transform.eulerAngles = new Vector3(0, yRot, 0);
        newWave.transform.localScale = size;
        MegaAttractorShape newAttractorShape = Water.AddComponent<MegaAttractorShape>();
        newAttractorShape.shape = newWave.GetComponent<MegaShapeCircle>();
        newAttractorShape.attractType = MegaAttractType.Attract;
        newAttractorShape.limit = 5;
        newAttractorShape.distance = 3;
        newAttractorShape.force = 3;
        Debug.Log(-newWave.transform.right * 50);
        newWave.transform.DOLocalMove(-newWave.transform.right * 50, moveTime).OnComplete(() =>
        {
            Destroy(newWave);
        });
    }

    public IEnumerator WaitAndControlNullAttractorShapes(GameObject ReflectorObject)
    {
        yield return new WaitForSeconds(.1f);

        MegaAttractorShape[] attractors = Water.GetComponents<MegaAttractorShape>();
        foreach (var item in attractors)
        {
            if (item.shape == null)
            {
                Destroy(item);
            }
        }
        yield return new WaitForSeconds(1f);
        ReflectorObject.GetComponent<Collider>().enabled = true;
    }

    public void CheckDestroyedPoints()
    {
        Collider[] colliders = Water.GetComponentsInChildren<Collider>();
        if (colliders.Length > 3)
        {
            Water.transform.DOMoveY(0, .5f).OnComplete(() =>
            {
                Destroy(Water);
            });
        }
    }
}
