using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private int expectedBalls = 10;
    [SerializeField] private Text ballsText;
    [SerializeField] private Text fallsText;
    [SerializeField] private Text coinsText;

    private int ballsCount = 0;
    private int fallsCount = 0;
    
    [SerializeField] private KeyCode menuKey;
    private bool gameOver = false;

    public static GameController instance = null;

    [SerializeField] private MenuScreen menuScreen;
    [SerializeField] private GameOverScreen gameOverScreen;
    [SerializeField] private ThirdPersonMovement playerController;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {   
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += RestartLevel;

        ballsText.text = "BALLS: " + ballsCount + "/" + expectedBalls;
        fallsText.text = "FALLS: " + fallsCount;
        coinsText.text = "COINS: " + PlayerPrefs.GetInt("playerCoins", 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(menuKey) && !gameOver && menuScreen != null)
        {
            menuScreen.SetUp();
        }
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= RestartLevel;
    }

    private void RestartLevel(Scene previousScene, Scene newScene)
    {
        gameOver = false;
        if (newScene.name != "LevelSelect")
        {
            menuScreen = FindObjectOfType<MenuScreen>(true);
            gameOverScreen = FindObjectOfType<GameOverScreen>(true);
            playerController = FindObjectOfType<ThirdPersonMovement>();

            Text[] texts = FindObjectsOfType<Text>();
            foreach (Text t in texts)
            {
                if (t.name == "FallsCount")
                {
                    fallsText = t;
                    fallsCount = 0;
                    fallsText.text = "FALLS: " + fallsCount;
                }
                else if (t.name == "BallsCount")
                {
                    ballsText = t;
                    ballsCount = 0;
                    ballsText.text = "BALLS: " + ballsCount + "/" + expectedBalls;
                }
                else if (t.name == "PlayerCoins")
                {
                    coinsText = t;
                    coinsText.text = "COINS: " + PlayerPrefs.GetInt("playerCoins", 0);
                }
            }
        }
    }

    public void CollectBall()
    {
        PlayerPrefs.SetInt("ballsCollected", PlayerPrefs.GetInt("ballsCollected")+1);

        ballsCount++;
        ballsText.text = "BALLS: " + ballsCount + "/" + expectedBalls;
        if(ballsCount >= expectedBalls)
        {
            GameOver();
        }
    }
    
    private void GameOver()
    {
        gameOver = true;

        int currentLevel = PlayerPrefs.GetInt("currentLevel", 0);
        if(currentLevel > PlayerPrefs.GetInt("reachedLevel", 0))
        {
            PlayerPrefs.SetInt("reachedLevel", currentLevel);
        }
        
        gameOverScreen.SetUp(fallsCount);
    }

    public void PlayerFall()
    {
        fallsCount++;
        fallsText.text = "FALLS: " + fallsCount;
        Invoke("PlayerReappear", 1f);
    }

    private void PlayerReappear()
    {
        playerController.Reappear();
    }

    public void SetPlayerCoins(int coins)
    {
        coinsText.text = "COINS: " + coins;
    }
}
