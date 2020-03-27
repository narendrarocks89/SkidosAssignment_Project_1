using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public GameObject Screen_1;
    public GameObject Screen_2;

    void OnEnable()
    {
        EventManager.ActivateScreen1Event += ActivateScreen1;
        EventManager.ActivateScreen2Event += ActivateScreen2;
    }

    void OnDisable()
    {
        EventManager.ActivateScreen1Event -= ActivateScreen1;
        EventManager.ActivateScreen2Event -= ActivateScreen2;
    }
    
    void ActivateScreen1(bool active)
    {
        Screen_1.SetActive(active);
        Screen_2.SetActive(!active);
    }

    void ActivateScreen2(bool active)
    {
        Screen_1.SetActive(!active);
        Screen_2.SetActive(active);
    }
}
