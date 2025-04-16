using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtensilItem : MonoBehaviour
{
    //Managers
    public SlicingManager SlicingManager;

    private bool sauceInSpoon;
    private GameObject sauceSelected;

    //Analytic collection
    public ChoppingEvent choppingEvent;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (transform.name)
            {
                case "Knife":
                    KnifeAction();
                    break;
                case "TBSP" or "TSP":          //More cases to add
                    MeasuringSpoonAction();
                    break;
                default:
                    break;
            }
        }
    }



    public void KnifeAction()
    {
        Collider[] collidedObject = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (Collider collider in collidedObject)
        {
            if(collider.transform.tag == "PickUpFood")      //Chopping food
            {
                if(collider.transform.TryGetComponent<SliceableObject>(out SliceableObject sliceableObject))
                {
                    //Analytic storage
                    StoreChoppingInfo(SlicingManager.choppingBoardColour, collider.transform.name);

                    GameObject slicedObject = sliceableObject.slicedVersion;

                    //Creating the cut version of the object
                    GameObject newItem = Instantiate(slicedObject, collider.transform.position, Quaternion.identity);
                    //newItem.transform.localScale = collider.transform.localScale;
                    if(!newItem.TryGetComponent<SliceableObject>(out SliceableObject sliced))
                    {
                        sliced = newItem.AddComponent<SliceableObject>();
                    }
                    sliced.hasBeenSliced = true;
                    newItem.tag = collider.transform.tag;
                    Destroy(collider.gameObject);
                }
            }
        }
    }

    public void MeasuringSpoonAction()
    {
        if (!sauceInSpoon)
        {
            Collider[] collidedObject = Physics.OverlapSphere(transform.position, 0.1f);
            foreach (Collider collider in collidedObject)
            {
                if (collider.transform.tag == "Sauce")      //Sauces
                {
                    sauceSelected = collider.transform.gameObject;
                    sauceSelected.transform.position = gameObject.transform.position;
                    sauceSelected.transform.parent = gameObject.transform;
                    if(transform.name == "TBSP")
                    {
                        sauceSelected.GetComponent<SauceObjectStorage>().amountOfSauce = 15;
                    }
                    else if(transform.name == "TSP")
                    {
                        sauceSelected.GetComponent<SauceObjectStorage>().amountOfSauce = 5;
                    }
                    sauceInSpoon = true;
                }
            }
        }
        else
        {
            Collider[] collidedObject = Physics.OverlapSphere(transform.position, 1f);
            foreach (Collider collider in collidedObject)
            {
                if (collider.transform.tag == "MixingBowl")
                {
                    sauceSelected.transform.position = collider.transform.position;
                    sauceSelected.transform.parent = collider.transform;            //Changing parent to mixing bowl
                    sauceInSpoon = false;
                }
            }
        }
    }

    
    
    //Storing analytics information
    public void StoreChoppingInfo(string boardColor, string itemName)
    {
        choppingEvent.Raise(new ChoppingEventData
        {
            boardColour = boardColor,
            cutItemName = itemName,
            isCorrect = true
        });
    }
}
