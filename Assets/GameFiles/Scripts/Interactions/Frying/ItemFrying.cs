using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFrying : MonoBehaviour
{
    private FryingManager fryingManager;
    public string itemName;

    public float timeInPan;

    public void Start()
    {
        fryingManager = GameObject.Find("FryingManager").GetComponent<FryingManager>();
        if (itemName == "Burger")       //Which pan the item goes in
        {
            transform.position = fryingManager.fryingPos1.position;
        }
        else
        {
            transform.position = fryingManager.fryingPos2.position;
        }
        timeInPan = 0;
    }

    public void Update()
    {
        timeInPan += Time.deltaTime;        //Count how long the item has been in the pan
    }
}
