using UnityEngine;

public class UtensilItem : MonoBehaviour
{
    //Managers
    public SlicingManager SlicingManager;

    //Variables
    private bool sauceInSpoon;
    private GameObject sauceSelected;

    //Analytic collection
    public ChoppingEvent choppingEvent;
    public SaucesEvent saucesEvent;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (transform.name)         //Finding out which utensil is being used
            {
                case "Knife":
                    KnifeAction();
                    break;
                case "TBSP" or "TSP":
                    MeasuringSpoonAction();
                    break;
                default:
                    break;
            }
        }
    }

    //Actions for utensils
    public void KnifeAction()
    {
        Collider[] collidedObject = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (Collider collider in collidedObject)
        {
            if(collider.transform.tag == "PickUpFood")      //Chopping food
            {
                if(collider.transform.TryGetComponent<SliceableObject>(out SliceableObject sliceableObject))
                {
                    if(sliceableObject.slicedVersion != null)
                    {
                        //Analytic storage
                        StoreChoppingInfo(SlicingManager.choppingBoardColour, collider.transform.name);

                        GameObject slicedObject = sliceableObject.slicedVersion;

                        //Creating the cut version of the object
                        GameObject newItem = Instantiate(slicedObject, collider.transform.position, Quaternion.identity);
                        if (!newItem.TryGetComponent<SliceableObject>(out SliceableObject sliced))
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
    }
    public void MeasuringSpoonAction()
    {
        //Picking up sauce
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

                    //Assigning the correct amount to the sauce
                    if(transform.name == "TBSP")
                    {
                        sauceSelected.GetComponent<SauceObjectStorage>().amountOfSauce = 15;
                    }
                    else if(transform.name == "TSP")
                    {
                        sauceSelected.GetComponent<SauceObjectStorage>().amountOfSauce = 10;
                    }
                    sauceInSpoon = true;
                }
            }
        }
        //Placing sauce in bowl
        else
        {
            Collider[] collidedObject = Physics.OverlapSphere(transform.position, 0.05f);
            foreach (Collider collider in collidedObject)
            {
                if (collider.transform.tag == "MixingBowl")
                {
                    //Analytic storage
                    StoreSauceInfo(sauceSelected.GetComponent<SauceObjectStorage>().amountOfSauce.ToString(), sauceSelected.transform.name);

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
        bool correctBoard = false;
        //Checking if correct board was used
        if(itemName == "MinceBeef" && boardColor == "Red")
        {
            correctBoard = true;
        }
        else if(itemName != "MinceBeef" && boardColor == "Green")
        {
            correctBoard = true;
        }

        //Raising the event to be called and analytics to be stored
        choppingEvent.Raise(new ChoppingEventData
        {
            boardColour = boardColor,
            cutItemName = itemName,
            isCorrect = correctBoard
        });
    }

    public void StoreSauceInfo(string amount, string sauceName)
    {
        //Checking if the correct amount of a specific sauce was used
        bool correctAmount = false;
        if (sauceName == "Mayo" || sauceName == "Lettuce(Clone)")
        {
            if(amount == "15")
            {
                correctAmount = true;
            }
        }
        else if (sauceName == "Tabasco" || sauceName == "Worcestershire" || sauceName == "Ketchup")
        {
            if (amount == "10")
            {
                correctAmount = true;
            }
        }

        //Raising the event to be called and analytics to be stored
        saucesEvent.Raise(new SaucesEventData
        {
            amount = amount,
            sauceName = sauceName,
            isCorrect = correctAmount
        });
    }
}
