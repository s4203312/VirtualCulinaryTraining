using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlicingManager : MonoBehaviour
{
    private bool isInteracting = false;
    public Camera slicingCam;

    //Colour Selection Variables
    public GameObject colourSelectionPrefab;
    private GameObject colourSelection;

    //
    public GameObject choppingBoard;
    public GameObject itemOnBoard;
    public Vector3 itemOnBoardOldPos;

    private void Start()
    {
        slicingCam.GetComponent<SelectingItems>().enabled = false;
        slicingCam.GetComponent<MouseSlicing>().enabled = false;
    }

    void Update()
    {
        //Showing colour options
        if (isInteracting == false && slicingCam.enabled == true)
        {
            ShowColourSelection();
        }
        //Choosing a colour for the board
        else if (colourSelection != null && colourSelection.GetComponent<SelectColour>().hitColour != null)
        {
            ChosenColour();
            slicingCam.GetComponent<SelectingItems>().enabled = true;

            //UIcanvas.SetActive(true);
        }

        //Selecting Items
        //if(slicingCam.GetComponent<SelectingItems>().selectedItem == )
        //{

        //}

    }

    //Colour chopping board functions
    private void ShowColourSelection()
    {
        isInteracting = true;
        Vector3 newPos = slicingCam.transform.position + slicingCam.transform.forward * 0.8f;
        colourSelection = Instantiate(colourSelectionPrefab, newPos, Quaternion.identity);
        colourSelection.GetComponent<SelectColour>().currentCam = slicingCam;
    }
    private void ChosenColour()
    {
        GameObject hitObject = colourSelection.GetComponent<SelectColour>().hitColour;
        Material chosenMaterial = hitObject.GetComponent<Renderer>().material;
        choppingBoard.GetComponent<Renderer>().material = chosenMaterial;
        Destroy(colourSelection);
    }
}
