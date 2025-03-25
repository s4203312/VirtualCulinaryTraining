using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dictionary<string, GameObject> gameCameras = new Dictionary<string, GameObject>();

    void Awake()
    {
        FindAllCameras();
        SetUpPlayer();
    }

    public void FindAllCameras()
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("Camera");

        gameCameras.Clear();
        gameCameras.Add(mainCamera.name, mainCamera);       //Adds the main player view as position 1
        mainCamera.GetComponent<Camera>().enabled = true;   //Setting it as starting camera

        foreach (GameObject camera in cameras)              //Adds all other camera views with referance names
        {
            gameCameras.Add(camera.name, camera);
            camera.GetComponent<Camera>().enabled = false;  //Disabling it to start
        }
    }
    private void SetUpPlayer()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
