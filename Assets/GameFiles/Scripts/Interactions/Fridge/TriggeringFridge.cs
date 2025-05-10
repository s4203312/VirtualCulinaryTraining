using UnityEngine;

public class TriggeringFridge : MonoBehaviour
{
    //Managers
    public GameManager Manager;

    //Analytic collection
    public FridgeEvent fridgeEvent;

    //Variables
    public GameObject fridge;
    public bool currentlyMoving;
    private bool fridgeEveFired = false;
    private float fridgeOpenTime;
    public Transform burgerPosByPan;

    private void OnTriggerStay(Collider other)      
    {
        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.E) == true && !currentlyMoving)    //Is player close to fridge and fridge not currently moving
        {
            currentlyMoving = true;
            fridge.GetComponent<OpenFridge>().OpenDoor();       //Open/Close the fridge door
        }

        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.F) == true 
            && !currentlyMoving && fridge.GetComponent<OpenFridge>().isOpen                     //Checks if the fridge is open and burgers are there so player can remove them
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
                fridgeEvent.Raise(new FridgeEventData { isCorrect = false });           //Fire event if fridge is left open
            }
        }
        else
        {
            fridgeOpenTime = 0;
        }
    }
}
