using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dictionary<string, GameObject> gameCameras = new Dictionary<string, GameObject>();
    public GameObject gameCanvases;

    public GameObject recipe;
    private string filePath;
    public float overallTimeTaken;

    public GameObject spaceInFridge;
    public GameObject itemInFridge;
    public bool isInFridge = false;

    void Awake()
    {
        FindAllCameras();
        SetUpPlayer();

        //Reseting time taken
        overallTimeTaken = 0;
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

    private void Update()
    {
        if(Input.GetKey(KeyCode.R) == true)     //Recipe
        {
            recipe.SetActive(true);
        }
        if (Input.GetKey(KeyCode.B) == true)
        {
            recipe.SetActive(false);
        }

        overallTimeTaken += Time.deltaTime;
    }
}
