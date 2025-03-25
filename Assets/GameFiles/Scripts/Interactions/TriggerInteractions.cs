using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInteractions : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject player;

    public bool inInteraction;

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
                inInteraction = false;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
