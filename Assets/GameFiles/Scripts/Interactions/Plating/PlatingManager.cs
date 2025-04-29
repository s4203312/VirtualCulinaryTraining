using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatingManager : MonoBehaviour
{
    public List<GameObject> preppedIngredients = new List<GameObject>();
    public GameObject prepSection;
    public GameObject burgerTop;
    public GameObject burgerBottom;
    public GameObject burgerSaucePrefab;
    public GameObject burgerSauce;

    public List<GameObject> correctPlatingOrder = new List<GameObject>();
    public List<GameObject> playerPlatingOrder = new List<GameObject>();

    private bool correct;

    //Analytic collection
    public PlatingEvent platingEvent;
    public PreppingEvent preppingEvent;

    public void UpdateIngredients()
    {
        int i = 0;
        preppedIngredients.Add(burgerTop);
        foreach(GameObject preppedIngredient in preppedIngredients)
        {
            preppedIngredient.tag = "Plating";
            i++;
        }
        playerPlatingOrder.Add(burgerBottom);       //Adding to player list

        if (preppedIngredients.Count == 6)
        {
            CreatingCorrectOrder();
            correct = true;

            //Analytics for prepping everything correctly
            StorePreppingInfo(true);
        }
        else
        {
            correct = false;        //Cant get the order correct without all ingredients

            //Analytics for incorrectly prepping
            StorePreppingInfo(false);
        }

        
    }

    public void PlateItem(GameObject item)
    {
        GameObject lastItem = playerPlatingOrder[playerPlatingOrder.Count - 1];
        if (item.name == "MainBowl")         //Spreading sauce
        {
            burgerSauce = Instantiate(burgerSaucePrefab);

            burgerSauce.transform.position = lastItem.transform.position + new Vector3(0, 0.05f, 0);
            item.SetActive(false);
        }
        else if (lastItem.name == "MainBowl")
        {
            item.transform.position = burgerSauce.transform.position + new Vector3(0, 0.05f, 0);
        }
        else
        {
            Debug.Log(lastItem);
            Debug.Log(lastItem.transform.position);
            item.transform.position = lastItem.transform.position + new Vector3(0,0.05f,0);
        }
        playerPlatingOrder.Add(item);       //Adding to player list
    }

    public void CreatingCorrectOrder()
    {
        correctPlatingOrder.Add(burgerBottom);      //Adds bottom to list
        correctPlatingOrder.Add(GameObject.Find("MainBowl"));
        correctPlatingOrder.Add(GameObject.Find("Tomato Slice(Clone)"));
        correctPlatingOrder.Add(GameObject.Find("Burger(Clone)"));
        correctPlatingOrder.Add(GameObject.Find("Bacon"));
        correctPlatingOrder.Add(GameObject.Find("Onion Half(Clone)"));
        correctPlatingOrder.Add(GameObject.Find("BunTop"));         
    }
    public void CheckPositions()
    {
        int i = 0;
        if (correct)
        {
            foreach (GameObject item in playerPlatingOrder)
            {
                if (item != correctPlatingOrder[i])
                {
                    correct = false;
                }
                i++;
            }
        }
        

        if(correct)
        {
            //Analytics for correct plating
            StorePlatingInfo(true);
        }
        else
        {
            //Analytics for wrong plating 
            StorePlatingInfo(false);
        }
    }


    //Storing analytics information
    public void StorePreppingInfo(bool isCorrect)
    {
        //Raising the event to be called and analytics to be stored
        preppingEvent.Raise(new PreppingEventData
        {
            isCorrect = isCorrect
        });
    }
    public void StorePlatingInfo(bool isCorrect)
    {
        //Raising the event to be called and analytics to be stored
        platingEvent.Raise(new PlatingEventData
        {
            isCorrect = isCorrect
        });

    }
}
