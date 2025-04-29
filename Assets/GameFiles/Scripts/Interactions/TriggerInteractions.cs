using System;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInteractions : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject player;

    public bool inInteraction;

    private GameObject currentInteractionManager;

    //Into the interaction view
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == player && Input.GetKey(KeyCode.E) == true)
        {
            gameManager.gameCameras.TryGetValue(transform.name, out GameObject camera);
            GameObject currentCanvas = gameManager.gameCanvases.transform.Find(transform.name).gameObject;
            if (camera != null)
            {
                player.GetComponent<CharacterController>().enabled = false;     //Sets the movement of character to false
                player.GetComponentInChildren<Camera>().enabled = false;

                camera.GetComponent<Camera>().enabled = true;
                currentInteractionManager = GameObject.Find(transform.name + "Manager");           //Finding the manager for interactions
                if(currentInteractionManager != null)
                {
                    Type managerScriptType = Type.GetType(transform.name + "Manager");
                    Component managerScript = currentInteractionManager.GetComponent(managerScriptType);
                    managerScript.GetType().GetProperty("enabled").SetValue(managerScript, true);

                    currentCanvas.SetActive(true);
                }

                inInteraction = true;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    //Out of interaction view
    private void Update()
    {
        if (inInteraction && Input.GetKey(KeyCode.B) == true)
        {
            gameManager.gameCameras.TryGetValue(transform.name, out GameObject camera);
            GameObject currentCanvas = gameManager.gameCanvases.transform.Find(transform.name).gameObject;
            if (camera != null)
            {
                player.GetComponent<CharacterController>().enabled = true;     //Sets the movement of character to false
                player.GetComponentInChildren<Camera>().enabled = true;

                camera.GetComponent<Camera>().enabled = false;

                //Reseting the section when leaving
                SelectingItems selectingScript = camera.gameObject.GetComponent<SelectingItems>();
                if (selectingScript.selectedItem != null)
                {
                    selectingScript.selectedItem.TryGetComponent(out UtensilItem script);
                    if (script != null)
                    {
                        script.enabled = false;
                        selectingScript.selectedItem.transform.position = selectingScript.utensilOldPos;
                    }
                    selectingScript.itemInHand = false;
                    selectingScript.selectedItem = null;
                }

                if (currentInteractionManager != null)
                {
                    Type managerScriptType = Type.GetType(transform.name + "Manager");
                    Component managerScript = currentInteractionManager.GetComponent(managerScriptType);
                    managerScript.GetType().GetProperty("enabled").SetValue(managerScript, false);

                    currentCanvas.SetActive(false);
                }
                inInteraction = false;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
