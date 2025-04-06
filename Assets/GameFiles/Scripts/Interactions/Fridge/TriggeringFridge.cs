using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeringFridge : MonoBehaviour
{
    public GameObject fridge;
    public bool open;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.E) == true && !open)
        {
            fridge.GetComponent<OpenFridge>().OpenDoor();
            open = true;
        }
    }
}
