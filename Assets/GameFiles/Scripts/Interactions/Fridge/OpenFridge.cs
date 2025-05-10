using System.Collections;
using UnityEngine;

public class OpenFridge : MonoBehaviour
{
    //Analytic collection
    public FridgeEvent fridgeEvent;

    //Variables
    public bool isOpen = false;
    public TriggeringFridge openTrigger;
    public GameObject fridgeCanvas;

    public void OpenDoor()
    {
        GameObject pivotPoint = transform.GetChild(0).gameObject;
        GameObject door = pivotPoint.transform.GetChild(0).gameObject;

        if (isOpen)     //Flipping open and closed variable
        {
            isOpen = false;
            fridgeCanvas.SetActive(false);          //Removing canvas
            fridgeEvent.Raise(new FridgeEventData { isCorrect = true });            //Recording the fridge has been closed
        }
        else
        {
            isOpen = true;
        }

        StartCoroutine(OpeningDoor(pivotPoint, door));

        
    }

    public IEnumerator OpeningDoor(GameObject pivotPoint, GameObject door)          //Slowly rotates the door to set position
    {
        float targetAngle = 100;
        float rotatedAngle = 0f;
        float rotationSpeed = 35f;

        while (rotatedAngle < targetAngle)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            if (isOpen)             //Either opening or closing
            {
                door.transform.RotateAround(pivotPoint.transform.position, Vector3.up, rotationStep);
            }
            else
            {
                door.transform.RotateAround(pivotPoint.transform.position, Vector3.down, rotationStep);
            }
            
            rotatedAngle += rotationStep;
            yield return null;
        }

        openTrigger.currentlyMoving = false;        //Allowing it to move again
        if (isOpen)
        {
            fridgeCanvas.SetActive(true);           //Turning canvas on when open
        }
    }
}
