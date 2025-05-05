using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonsFeedback : MonoBehaviour
{
    public GameObject retryButton;
    public GameObject menuButton;

    void Start()
    {
        if(PlayerPrefs.GetInt("FirstAttempt") == 1)
        {
            retryButton.SetActive(true);
            menuButton.SetActive(false);
        }
        else
        {
            retryButton.SetActive(false);
            menuButton.SetActive(true);
        }
    }
}
