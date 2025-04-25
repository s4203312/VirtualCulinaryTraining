using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ItemFrying : MonoBehaviour
{
    private FryingManager fryingManager;
    public string itemName;
    public Material colouredBurger;
    public Material burntBurger;

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
        if(timeInPan > 10f && timeInPan < 19.9f)
        {
            transform.GetChild(0).GetComponent<MeshRenderer>().material = transform.GetComponent<ItemFrying>().colouredBurger;
        }
        else if(timeInPan > 20f)
        {
            transform.GetChild(0).GetComponent<MeshRenderer>().material = transform.GetComponent<ItemFrying>().burntBurger;
        }
        fryingManager.ProgressBar(timeInPan);
    }
}
