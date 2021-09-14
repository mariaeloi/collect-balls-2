using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{
    [SerializeField] Timer timer;

    public void SetUp()
    {
        if (!gameObject.activeInHierarchy)
        {
            timer.Pause();
            Cursor.lockState = CursorLockMode.None;
            FindObjectOfType<ThirdPersonMovement>().SetActive(false);
            gameObject.SetActive(true);
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResumeButton()
    {  
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        FindObjectOfType<ThirdPersonMovement>().SetActive(true);
        timer.Play();
    }

    public void LevelSelectButton()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
