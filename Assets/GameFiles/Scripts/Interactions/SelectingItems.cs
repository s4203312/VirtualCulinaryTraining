using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectingItems : MonoBehaviour
{
    public SlicingManager manager;
    public PrepManager prepManager;
    
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
                        selectedItem.transform.position = manager.finishedBowl.transform.position + manager.finishedBowl.transform.up * 0.1f;
                        selectedItem.transform.parent = manager.finishedBowl.transform;
                        StartCoroutine(moveBowl(selectedItem));

                        //Reseting variables
                        manager.itemOnBoard = null;
                    }
                    else
                    {
                        if (manager.itemOnBoard != null)            //Putting item back
                        {
                            manager.itemOnBoard.transform.position = manager.itemOnBoardOldPos;
                        }

                        //Selecting the item
                        selectedItem = hit.collider.gameObject;
                        manager.itemOnBoardOldPos = selectedItem.transform.position;
                        manager.itemOnBoard = selectedItem;

                        //Moving item to board
                        selectedItem.transform.position = manager.choppingBoard.transform.position + manager.choppingBoard.transform.up * 0.1f;
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
        prepManager.preppedIngredients.Add(preppedItem);
        preppedItem.SetActive(false);
        yield return null;
    } 
}
