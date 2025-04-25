using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeringFridge : MonoBehaviour
{
    public GameManager Manager;

    //Analytic collection
    public FridgeEvent fridgeEvent;

    public GameObject fridge;
    public bool currentlyMoving;
    private bool fridgeOpen = false;
    private float fridgeOpenTime;

    public Transform burgerPosByPan;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.E) == true && !currentlyMoving)
        {
            currentlyMoving = true;
            fridgeOpen = true;
            fridge.GetComponent<OpenFridge>().OpenDoor();
        }

        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.F) == true 
            && !currentlyMoving && fridge.GetComponent<OpenFridge>().isOpen 
            && Manager.isInFridge)
        {
            //Burger move functionality
            Manager.itemInFridge.tag = "Fryable";
            Manager.itemInFridge.transform.position = burgerPosByPan.position;
            Manager.itemInFridge.transform.rotation = burgerPosByPan.rotation;
        }
    }

    private void Update()
    {
        if (fridgeOpen)
        {
            fridgeOpenTime += Time.deltaTime;        //Count how long the item has been in the pan
            if (fridgeOpenTime > 20f)
            {
                fridgeOpen = false;
                fridgeEvent.Raise(new FridgeEventData { isCorrect = true });
            }
        }
    }
}
