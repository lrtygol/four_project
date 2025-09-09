using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    public Slider slider;
    public void set_health(int health)
    {
        Debug.Log(health);
        slider.value = health;

    }
    
}
