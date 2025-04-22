using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectingItems : MonoBehaviour
{
    public GameManager gameManager;
    public SlicingManager slicingManager;
    public PrepManager prepManager;
    public SaucesManager saucesManager;
    public FryingManager fryingManager;
    
    public GameObject selectedItem = null;

    public bool itemInHand;
    public Vector3 utensilOldPos;

    private float zPosition;
    private Vector3 offset;


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
            if (Input.GetMouseButtonDown(1))
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

    void SelectItem()
    {
        if (GetComponent<Camera>().enabled)
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("PickUpFood"))      //Unable to move food just select it
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
                else if (hit.collider.CompareTag("PickUpUtensil"))      //Able to move utensils
                {
                    itemInHand = true;
                    selectedItem = hit.collider.gameObject;
                    utensilOldPos = selectedItem.transform.position;
                    selectedItem.GetComponent<UtensilItem>().enabled = true;

                    zPosition = GetComponent<Camera>().WorldToScreenPoint(selectedItem.transform.position).z; //Locks the z position
                    offset = selectedItem.transform.position - GetMouseWorldPosition();
                }
                else if (hit.collider.CompareTag("Fryable"))            //Selecting burgers and bacon
                {
                    if(hit.transform.GetComponent<ItemFrying>().enabled == false)
                    {
                        hit.transform.GetComponent<ItemFrying>().enabled = true;    //Starting process. Putting into pan
                    }
                    else
                    {
                        string itemName = hit.transform.GetComponent<ItemFrying>().itemName;
                        Debug.Log("Burger in pan: " + hit.transform.GetComponent<ItemFrying>().timeInPan);
                        hit.transform.GetComponent<ItemFrying>().enabled = false;
                        fryingManager.MoveItemToPrep(itemName, hit.transform.gameObject);
                    }
                }
            }
        }
    }

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
            prepManager.preppedIngredients.Add(preppedItem);
            preppedItem.SetActive(false);
        }
        yield return null;
    } 
}
