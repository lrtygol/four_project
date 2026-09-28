using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class health : MonoBehaviour
{
    public GameObject DieScreen;
    public GameObject StartScreen;
    public Slider slider;
    void Start()
    {
        StartScreen.SetActive(true);
    }
    public void set_health(int health)
    {
        Debug.Log(health);
        slider.value = health;
        if (health <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            DieScreen.SetActive(true);
            Time.timeScale = 0;
            
        }
    }

    public void StartGame()
    {
        StartScreen.SetActive(false);
        Debug.Log("да");
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }










    //IEnumerator ReloadSceneWithDelay()
    //{

    //    yield return new WaitForSecondsRealtime(2f);


    //    Time.timeScale = 1f;


    //    SceneManager.LoadScene("BossFight");

    //}
}
