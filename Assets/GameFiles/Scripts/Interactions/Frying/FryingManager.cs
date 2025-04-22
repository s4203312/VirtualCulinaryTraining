using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryingManager : MonoBehaviour
{
    public Camera fryingCam;

    public Transform fryingPos1;
    public Transform fryingPos2;

    public GameObject burgerPlatingPos;
    public GameObject baconPlatingPos;

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
}
