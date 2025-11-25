using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class health : MonoBehaviour
{
    public Slider slider;
    public void set_health(int health)
    {
        Debug.Log(health);
        slider.value = health;
        if (health <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            //DieScreen.SetActive(true);
            Time.timeScale = 0;
            StartCoroutine(ReloadSceneWithDelay());
        }
    }
    IEnumerator ReloadSceneWithDelay()
    {

        yield return new WaitForSecondsRealtime(2f);


        Time.timeScale = 1f;


        SceneManager.LoadScene("BossFight");

    }

}
