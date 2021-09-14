using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelInfoScreen levelInfoScreen;
    private Button[] levelButtons;
    
    void Awake()
    {
        int reachedLevel = PlayerPrefs.GetInt("reachedLevel", 0);

        levelButtons = new Button[transform.childCount];
        for(int i=0; i<levelButtons.Length; i++)
        {
            levelButtons[i] = transform.GetChild(i).GetComponent<Button>();
            levelButtons[i].GetComponentInChildren<Text>().text = "Level " + (i + 1);
            if(i > reachedLevel)
            {
                levelButtons[i].interactable = false;
            }
        }
        PlayerPrefs.SetInt("totalLevels", levelButtons.Length);
    }

    public void LoadInfoLevel(int level)
    {
        levelInfoScreen.SetUp(level);
        //PlayerPrefs.SetInt("currentLevel", level);
        //SceneManager.LoadScene("Level" + level);
    }
}
