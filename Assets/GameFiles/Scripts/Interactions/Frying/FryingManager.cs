using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FryingManager : MonoBehaviour
{
    public Camera fryingCam;

    public Transform fryingPos1;
    public Transform fryingPos2;

    public GameObject burgerPlatingPos;
    public GameObject baconPlatingPos;

    public Image progressBarImg;

    public void MoveItemToPrep(string itemName, GameObject itemHit)
    {
        if(itemName == "Burger")
        {
            itemHit.transform.position = burgerPlatingPos.transform.position;
        }
        else
        {
            itemHit.transform.position = baconPlatingPos.transform.position;
        }
    }

    public void ProgressBar(float currentTime)
    {
        float burntTime = 20f;
        if(currentTime >= burntTime)
        {
            progressBarImg.fillAmount = 1;      //Fully burnt
        }
        else
        {
            progressBarImg.fillAmount = currentTime / burntTime;        //Progress update
        }
    }
}
