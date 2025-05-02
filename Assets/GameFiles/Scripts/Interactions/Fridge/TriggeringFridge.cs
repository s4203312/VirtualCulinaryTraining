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
    private bool fridgeEveFired = false;
    private float fridgeOpenTime;

    public Transform burgerPosByPan;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.E) == true && !currentlyMoving)
        {
            currentlyMoving = true;
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
        if (fridge.GetComponent<OpenFridge>().isOpen && !fridgeEveFired)
        {
            fridgeOpenTime += Time.deltaTime;        //Count how long the fridge has been open
            if (fridgeOpenTime > 20f)
            {
                fridgeEveFired = true;
                fridgeEvent.Raise(new FridgeEventData { isCorrect = false });
            }
        }
        else
        {
            fridgeOpenTime = 0;
        }
    }
}
