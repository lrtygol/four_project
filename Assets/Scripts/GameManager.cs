using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject DieScreen;
    public void StartGame()
    {
        StartScreen.SetActive(false);
        Debug.Log("да");
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void EndGame()
    {
        DieScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
