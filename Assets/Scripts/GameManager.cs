﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        Application.targetFrameRate = 60;


        PlayerPrefs.SetInt("FromMenu", 1);
        if (PlayerPrefs.GetInt("FromMenu") == 1)
        {
            ContinueText.SetActive(false);
            PlayerPrefs.SetInt("FromMenu", 0);
        }
        else
        {
            PlayText.SetActive(false);
        }
        currentLevel = PlayerPrefs.GetInt("LevelId");
        LevelText.text = "LEVEL " + currentLevel.ToString();
    }

    public int currentLevel = 1;
    int MaxLevelNumber = 2;
    public bool isGameStarted, isGameOver;

    #region UI Elements
    public GameObject WinPanel, LosePanel, InGamePanel;
    public Button TapToStartButton;
    public Text LevelText;
    public GameObject PlayText, ContinueText;
    #endregion

    public PlayerController playerController;
    public GameObject Water;
    public GameObject WavePrefab;
    GameObject newWave;

    public void CreateWave(Transform parent, Vector3 position, float yRot, Vector3 size)
    {
        newWave = Instantiate(WavePrefab, position, Quaternion.identity, parent);
        newWave.transform.localEulerAngles = new Vector3(0, yRot, 0);
        newWave.transform.localScale = new Vector3(.1f, .1f, .1f);
        newWave.transform.DOScale(size, 2);
        MegaAttractorShape newAttractorShape = Water.AddComponent<MegaAttractorShape>();
        newAttractorShape.shape = newWave.GetComponentInChildren<MegaShapeCircle>();
        newAttractorShape.attractType = MegaAttractType.Attract;
        newAttractorShape.limit = 5;
        newAttractorShape.distance = 3;
        newAttractorShape.force = 3;
        newWave.GetComponentInChildren<MegaShapeCircle>().isMoving = true;
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

    public IEnumerator WaitAndGameWin()
    {
        Debug.Log("Win");
        isGameOver = true;

        //SoundManager.Instance.StopAllSounds();
        //SoundManager.Instance.playSound(SoundManager.GameSounds.Win);

        yield return new WaitForSeconds(1f);

        //if (PlayerPrefs.GetInt("VIBRATION") == 1)
        //    TapticManager.Impact(ImpactFeedback.Light);

        currentLevel++;
        PlayerPrefs.SetInt("LevelId", currentLevel);
        WinPanel.SetActive(true);
    }

    public IEnumerator WaitAndGameLose()
    {
        Debug.Log("Lose");
        isGameOver = true;

        //SoundManager.Instance.playSound(SoundManager.GameSounds.Lose);

        yield return new WaitForSeconds(1f);

        //if (PlayerPrefs.GetInt("VIBRATION") == 1)
        //    TapticManager.Impact(ImpactFeedback.Medium);

        LosePanel.SetActive(true);
    }

    public void TapToNextButtonClick()
    {
        if (currentLevel > MaxLevelNumber)
        {
            int rand = Random.Range(1, MaxLevelNumber);
            if (rand == PlayerPrefs.GetInt("LastRandomLevel"))
            {
                rand = Random.Range(1, MaxLevelNumber);
            }
            else
            {
                PlayerPrefs.SetInt("LastRandomLevel", rand);
            }
            SceneManager.LoadScene("Level" + rand);
        }
        else
        {
            SceneManager.LoadScene("Level" + currentLevel);
        }
    }

    public void TapToTryAgainButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TapToStartButtonClick()
    {
        isGameStarted = true;
        TapToStartButton.gameObject.SetActive(false);
    }
}