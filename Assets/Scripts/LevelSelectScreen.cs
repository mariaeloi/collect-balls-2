using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectScreen : MonoBehaviour
{
    [SerializeField] private Text ballsCollected;
    [SerializeField] private Text playerCoins;

    void Start()
    {
        ballsCollected.text = "Total balls collected: " + PlayerPrefs.GetInt("ballsCollected", 0);
        playerCoins.text = "Coins: " + PlayerPrefs.GetInt("playerCoins", 0);
    }

    public void QuitGame()
    {
        Debug.Log("Quit game.");
        Application.Quit();
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("reachedLevel", 0);
        PlayerPrefs.SetInt("ballsCollected", 0);
        PlayerPrefs.SetInt("playerCoins", 0);

        int totalLevels = PlayerPrefs.GetInt("totalLevels", 0);
        for (int level = 1; level <= totalLevels; level++)
        {
            PlayerPrefs.SetInt("recordFalls"+level, -1);
            PlayerPrefs.SetInt("minTimeRecordFalls"+level, -1);
            PlayerPrefs.SetFloat("secTimeRecordFalls"+level, -1);
        }

        SceneManager.LoadScene("LevelSelect");
    }
}
