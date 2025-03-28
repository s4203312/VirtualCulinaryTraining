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
            if(camera != null)
            {
                player.GetComponent<CharacterController>().enabled = false;     //Sets the movement of character to false
                player.GetComponentInChildren<Camera>().enabled = false;

                camera.GetComponent<Camera>().enabled = true;
                Debug.Log(transform.name + "Manager");
                currentInteractionManager = GameObject.Find(transform.name + "Manager");           //Finding the manager for interactions
                if(currentInteractionManager != null)
                {
                    Type managerScriptType = Type.GetType(transform.name + "Manager");
                    Component managerScript = currentInteractionManager.GetComponent(managerScriptType);
                    managerScript.GetType().GetProperty("enabled").SetValue(managerScript, true);
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
            if (camera != null)
            {
                player.GetComponent<CharacterController>().enabled = true;     //Sets the movement of character to false
                player.GetComponentInChildren<Camera>().enabled = true;

                camera.GetComponent<Camera>().enabled = false;
                if (currentInteractionManager != null)
                {
                    currentInteractionManager.SetActive(false);
                }
                inInteraction = false;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
