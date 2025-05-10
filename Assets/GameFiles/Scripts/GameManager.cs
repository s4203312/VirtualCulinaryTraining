using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variables
    public Dictionary<string, GameObject> gameCameras = new Dictionary<string, GameObject>();
    public GameObject gameCanvases;
    public GameObject recipe;
    public float overallTimeTaken;
    public GameObject spaceInFridge;
    public GameObject itemInFridge;
    public bool isInFridge = false;

    void Awake()
    {
        //Setting up the game
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
        Cursor.lockState = CursorLockMode.Locked;       //Ensuring the cursor is not visable
        Cursor.visible = false;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.R) == true)     //Opening the recipe
        {
            recipe.SetActive(true);
        }
        if (Input.GetKey(KeyCode.T) == true)
        {
            recipe.SetActive(false);
        }

        overallTimeTaken += Time.deltaTime;     //Updating the total time
    }
}
