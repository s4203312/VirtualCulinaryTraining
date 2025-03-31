using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private bool isBeingHeld = false;
    private Transform holdPos;


    void Update()
    {
        if (isBeingHeld && holdPos != null)
        {
            transform.position = holdPos.position;  //Updating the hold position
        }
    }

    public void PickUpItem(Transform hand)
    {
        isBeingHeld = true;
        holdPos = hand;
    }

    public void DropItem()
    {
        isBeingHeld = false;
    }
}
