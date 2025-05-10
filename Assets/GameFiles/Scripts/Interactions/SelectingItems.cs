using System.Collections;
using UnityEngine;

public class SelectingItems : MonoBehaviour
{
    //Managers
    public GameManager gameManager;
    public SlicingManager slicingManager;
    public PlatingManager platingManager;
    public SaucesManager saucesManager;
    public FryingManager fryingManager;
    
    //Variables
    public GameObject selectedItem = null;
    public bool itemInHand;
    public Vector3 utensilOldPos;
    private float zPosition;
    private Vector3 offset;
    private int i;

    //Analytic collection
    public FryingEvent fryingEvent;
    public CheeseEvent cheeseEvent;


    private void Update()
    {
        //Interacting with items
        if (Input.GetMouseButtonDown(0) && !itemInHand)
        {
            SelectItem();
        }

        //Moving item if a utensil
        if (selectedItem != null && selectedItem.tag == "PickUpUtensil")
        {
            if (Input.GetMouseButtonDown(1))        //Putting utensil down
            {
                selectedItem.transform.position = utensilOldPos;
                selectedItem.GetComponent<UtensilItem>().enabled = false;
                selectedItem = null;
                itemInHand = false;
            }
            else
            {
                MoveItem();
            }
        }
    }

    //Picking up an item
    void SelectItem()
    {
        if (GetComponent<Camera>().enabled)
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                switch (hit.transform.tag)
                {
                    case "PickUpFood":      //Unable to move food just select it
                        PickUpFood(hit);
                        break;
                    case "PickUpUtensil":       //Able to move utensils
                        PickUpUtensil(hit);
                        break;
                    case "Fryable":             //Selecting burgers and bacon
                        PickUpFryable(hit);
                        break;
                    case "Plating":             //Finally plating
                        PickUpPlating(hit);
                        break;
                    default:                    //Clicking on nothing of importance
                        break;
                }
            }
        }
    }

    //Moves item across screen
    void MoveItem()
    {
        selectedItem.transform.position = GetMouseWorldPosition() + offset;
    }

    //Locks z position to board
    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zPosition; //Brings object inline with board
        return GetComponent<Camera>().ScreenToWorldPoint(mousePoint);
    }

    private IEnumerator moveBowl(GameObject preppedItem)
    {
        yield return new WaitForSeconds(1f);
        if(preppedItem.TryGetComponent(out SauceObjectStorage sauceScript))      //Lettuce going to sauce station
        {
            Destroy(preppedItem.GetComponent<SliceableObject>());
            preppedItem.tag = "Sauce";
            preppedItem.transform.position = saucesManager.lettuceBowl.transform.position + saucesManager.lettuceBowl.transform.up * 0.1f;
        }
        else if(preppedItem.TryGetComponent(out placeHolderFrying fryScript))                       //Burgers going to fridge
        {
            Destroy(preppedItem.GetComponent<SliceableObject>());
            preppedItem.transform.position = gameManager.spaceInFridge.transform.position;
            gameManager.itemInFridge = preppedItem;
            gameManager.isInFridge = true;
        }
        else                //All other items to prep station
        {
            platingManager.preppedIngredients.Add(preppedItem);
            preppedItem.transform.position = platingManager.prepSection.transform.GetChild(i).GetChild(0).position;
            preppedItem.tag = "Untagged";
            i++;
        }
        yield return null;
    } 

    //Functions for interactions
    private void PickUpFood(RaycastHit hit)
    {
        if (hit.transform.GetComponent<SliceableObject>().hasBeenSliced)    //Moving sliced items to bowl
        {
            selectedItem = hit.collider.gameObject;
            selectedItem.transform.position = slicingManager.finishedBowl.transform.position + slicingManager.finishedBowl.transform.up * 0.1f;
            StartCoroutine(moveBowl(selectedItem));

            //Reseting variables
            slicingManager.itemOnBoard = null;
        }
        else
        {
            if (slicingManager.itemOnBoard != null)            //Putting item back
            {
                slicingManager.itemOnBoard.transform.position = slicingManager.itemOnBoardOldPos;
            }

            //Selecting the item
            selectedItem = hit.collider.gameObject;
            slicingManager.itemOnBoardOldPos = selectedItem.transform.position;
            slicingManager.itemOnBoard = selectedItem;

            //Moving item to board
            selectedItem.transform.position = slicingManager.choppingBoard.transform.position + slicingManager.choppingBoard.transform.up * 0.1f;
        }
    }
    private void PickUpUtensil(RaycastHit hit)
    {
        itemInHand = true;
        selectedItem = hit.collider.gameObject;
        utensilOldPos = selectedItem.transform.position;
        selectedItem.GetComponent<UtensilItem>().enabled = true;

        zPosition = GetComponent<Camera>().WorldToScreenPoint(selectedItem.transform.position).z; //Locks the z position
        offset = selectedItem.transform.position - GetMouseWorldPosition();
    } 
    private void PickUpFryable(RaycastHit hit) 
    {
        if (hit.transform.GetComponent<ItemFrying>().enabled == false)
        {
            hit.transform.GetComponent<ItemFrying>().enabled = true;    //Starting process. Putting into pan
        }
        else
        {
            string itemName = hit.transform.GetComponent<ItemFrying>().itemName;            //Taking fry item out
            if (itemName == "Burger")
            {
                if (hit.transform.childCount <= 1)       //Is cheese on burger?
                {
                    //Analytic storage
                    StoreCheeseInfo(false);
                }
            }

            //Analytic storage
            StoreFryingInfo(hit.transform.GetComponent<ItemFrying>().timeInPan, itemName);

            //Reseting and adding to prep section
            hit.transform.GetComponent<ItemFrying>().timeInPan = 0;
            hit.transform.GetComponent<ItemFrying>().enabled = false;
            fryingManager.MoveItemToPrep(itemName, hit.transform.gameObject);
            platingManager.preppedIngredients.Add(hit.transform.gameObject);
            hit.transform.tag = "Untagged";
        }
    }
    private void PickUpPlating(RaycastHit hit)
    {
        platingManager.PlateItem(hit.transform.gameObject);     //Putting item into burger stack
    }

    //Storing analytics information
    public void StoreFryingInfo(float time, string itemName)
    {
        bool correctCooking = false;
        //Checking if correct time was taken
        if (time < 20 && time > 10)
        {
            correctCooking = true;
        }

        //Raising the event to be called and analytics to be stored
        fryingEvent.Raise(new FryingEventData
        {
            cookingTime = time.ToString(),
            itemName = itemName,
            isCorrect = correctCooking
        });
    }

    public void StoreCheeseInfo(bool isCorrect)
    {
        //Raising the event to be called and analytics to be stored
        cheeseEvent.Raise(new CheeseEventData
        {
            isCorrect = isCorrect
        });
    }
}
