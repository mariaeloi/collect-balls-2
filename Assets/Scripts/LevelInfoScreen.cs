using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelInfoScreen : MonoBehaviour
{
    [SerializeField] private Text recordText;
    [SerializeField] private Text levelName;

    private int numberLevel;

    public void SetUp(int level)
    {
        numberLevel = level;
        levelName.text = "Level " + level;

        int recordFalls = PlayerPrefs.GetInt("recordFalls"+level, -1);
        int min = PlayerPrefs.GetInt("minTimeRecordFalls"+level, -1);
        float sec = PlayerPrefs.GetFloat("secTimeRecordFalls"+level, -1);
        if(recordFalls == -1)
        {
            recordText.text = "Record: Try it once!";
        }
        else
        {
            recordText.text = "Record: " + recordFalls + " falls (" + min.ToString("00") + ":" + sec.ToString("00") + ")";
        }

        gameObject.SetActive(true);
    }

    public void PlayButton()
    {
        PlayerPrefs.SetInt("currentLevel", numberLevel);
        SceneManager.LoadScene("Level" + numberLevel);
    }

    public void BackButton()
    {
        gameObject.SetActive(false);
    }
}
