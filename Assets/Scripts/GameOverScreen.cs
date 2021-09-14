using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Text recordText;
    [SerializeField] private Text fallsText;
    [SerializeField] private Text message;
    [SerializeField] private Text rewardText;

    [SerializeField] private Timer timer;

    private bool TimerIsShorter(int minutes, float seconds)
    {
        bool isShorter = false;

        if(timer.GetMinutes() < minutes|| (timer.GetMinutes() == minutes && timer.GetSeconds() < seconds))
        {
            isShorter = true;
        }

        return isShorter;
    }

    public void SetUp(int fallsCount)
    {
        timer.Pause();
        int level = PlayerPrefs.GetInt("currentLevel", 0);
        int recordFalls = PlayerPrefs.GetInt("recordFalls"+level, -1);
        int min = PlayerPrefs.GetInt("minTimeRecordFalls"+level, -1);
        float sec = PlayerPrefs.GetFloat("secTimeRecordFalls"+level, -1);

        if (recordFalls == -1 || recordFalls >= fallsCount)
        {
            if (min == -1 || sec == -1 || !(recordFalls == fallsCount && !TimerIsShorter(min, sec)))
            {
                min = timer.GetMinutes();
                sec = timer.GetSeconds();
                PlayerPrefs.SetInt("minTimeRecordFalls"+level, min);
                PlayerPrefs.SetFloat("secTimeRecordFalls"+level, sec);
            }
            
            recordFalls = fallsCount;
            PlayerPrefs.SetInt("recordFalls"+level, recordFalls);
        }
        recordText.text = "Record: " + recordFalls + " falls (" + min.ToString("00") + ":" + sec.ToString("00") + ")";

        fallsText.text = fallsCount + " FALLS";
        if(fallsCount == 0)
        {
            message.text = "No falls, amazing!";
        }
        else
        {
            message.text = "Try again without any falls!";
        }

        int coins = 1000 / (timer.GetMinutes() + 1);
        rewardText.text = "REWARD: " + coins;
        PlayerPrefs.SetInt("playerCoins", PlayerPrefs.GetInt("playerCoins", 0) + coins);

        Cursor.lockState = CursorLockMode.None;
        FindObjectOfType<ThirdPersonMovement>().SetActive(false);
        gameObject.SetActive(true);
    }

    public void PlayAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelSelectButton()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
