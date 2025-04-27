using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ItemFrying : MonoBehaviour
{
    private FryingManager fryingManager;
    public string itemName;
    public Material colouredItem;
    public Material burntItem;

    public Image progressBarImg;
    public float timeInPan;

    //Analytic collection
    public CheeseEvent cheeseEvent;

    public void Start()
    {
        fryingManager = GameObject.Find("FryingManager").GetComponent<FryingManager>();
        if (itemName == "Burger")       //Which pan the item goes in
        {
            transform.position = fryingManager.fryingPos1.position;
            progressBarImg = fryingManager.burgerProgressBar;
            GameObject.Find("Cheese Thin").tag = "Fryable";
        }
        else if (itemName == "Cheese")
        {
            GameObject burger = GameObject.Find("Burger(Clone)");
            if (burger != null)
            {
                if (burger.GetComponent<ItemFrying>().timeInPan < 20)        //Checking if burger is in the pan
                {
                    Destroy(GetComponent<ItemFrying>());

                    transform.parent = burger.transform;
                    transform.position = burger.transform.position + new Vector3(0, 0.02f, 0);

                    //Analytic storage
                    StoreCheeseInfo(true);
                }
            }
        }
        else
        {
            transform.position = fryingManager.fryingPos2.position;
            progressBarImg = fryingManager.baconProgressBar;
        }
        timeInPan = 0;
    }

    public void Update()
    {
        timeInPan += Time.deltaTime;        //Count how long the item has been in the pan
        if (timeInPan > 10f && timeInPan < 19.9f)
        {
            transform.GetChild(0).GetComponent<MeshRenderer>().material = transform.GetComponent<ItemFrying>().colouredItem;
        }
        else if (timeInPan > 20f)
        {
            transform.GetChild(0).GetComponent<MeshRenderer>().material = transform.GetComponent<ItemFrying>().burntItem;
        }
        fryingManager.ProgressBar(timeInPan, progressBarImg);
    }


    //Storing analytics information
    public void StoreCheeseInfo(bool isCorrect)
    {
        //Raising the event to be called and analytics to be stored
        cheeseEvent.Raise(new CheeseEventData
        {
            isCorrect = isCorrect
        });
    }
}
