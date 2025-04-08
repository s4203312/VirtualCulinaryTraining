using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeringFridge : MonoBehaviour
{
    public GameManager Manager;

    public GameObject fridge;
    public bool currentlyMoving;

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
            Manager.itemInFridge.transform.position = burgerPosByPan.position;
            Manager.itemInFridge.transform.rotation = burgerPosByPan.rotation;
            Debug.Log("Burger move");
        }
    }
}
