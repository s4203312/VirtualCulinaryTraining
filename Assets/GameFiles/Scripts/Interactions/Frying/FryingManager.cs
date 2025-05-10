using UnityEngine;
using UnityEngine.UI;

public class FryingManager : MonoBehaviour
{
    //Variables
    public Camera fryingCam;
    public Transform fryingPos1;
    public Transform fryingPos2;
    public GameObject burgerPlatingPos;
    public GameObject baconPlatingPos;
    public Image burgerProgressBar;
    public Image baconProgressBar;

    public void MoveItemToPrep(string itemName, GameObject itemHit)     //Taking item to prep section
    {
        if(itemName == "Burger")
        {
            itemHit.transform.position = burgerPlatingPos.transform.position;
        }
        else if(itemName == "Bacon")
        {
            itemHit.transform.position = baconPlatingPos.transform.position;
        }
    }

    public void ProgressBar(float currentTime, Image progressBarImg)            //Updating progress bar
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
