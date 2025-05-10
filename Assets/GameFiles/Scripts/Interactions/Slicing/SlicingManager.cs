using UnityEngine;
using UnityEngine.UI;

public class SlicingManager : MonoBehaviour
{
    private bool isInteracting = false;
    public Camera slicingCam;
    public Canvas slicingCanvas;

    //Colour Selection Variables
    public GameObject colourSelectionPrefab;
    private GameObject colourSelection;

    //Chopping board information
    public GameObject choppingBoard;
    public string choppingBoardColour;
    public GameObject itemOnBoard;
    public Vector3 itemOnBoardOldPos;

    //Bowl information
    public GameObject finishedBowl;

    private void Start()
    {
        slicingCam.GetComponent<SelectingItems>().enabled = false;
        slicingCanvas.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(ChooseColourAgain);  //Assigning the function for changing the board
    }

    void Update()
    {
        //Showing colour options
        if (isInteracting == false && slicingCam.enabled == true)
        {
            slicingCanvas.enabled = false;
            ShowColourSelection();
        }
        //Choosing a colour for the board
        else if (colourSelection != null && colourSelection.GetComponent<SelectColour>().hitColour != null)
        {
            ChosenColour();
            slicingCam.GetComponent<SelectingItems>().enabled = true;
            slicingCanvas.enabled = true;
        }
    }

    //Colour chopping board functions
    private void ShowColourSelection()
    {
        isInteracting = true;
        Vector3 newPos = slicingCam.transform.position + slicingCam.transform.forward * 0.8f;
        colourSelection = Instantiate(colourSelectionPrefab, newPos, Quaternion.identity);      //Shows the options as balls on the screen
        colourSelection.GetComponent<SelectColour>().currentCam = slicingCam;
    }
    private void ChosenColour()
    {
        GameObject hitObject = colourSelection.GetComponent<SelectColour>().hitColour;
        Material chosenMaterial = hitObject.GetComponent<Renderer>().material;
        choppingBoard.GetComponent<Renderer>().material = chosenMaterial;
        choppingBoardColour = chosenMaterial.name;
        choppingBoardColour = choppingBoardColour.Substring(0, choppingBoardColour.LastIndexOf(" (Instance)"));     //Removes the the unneeded words from the end
        Destroy(colourSelection);
    }

    private void ChooseColourAgain()        //Reseting the board colour
    {
        if (itemOnBoard)
        {
            itemOnBoard.transform.position = itemOnBoardOldPos;
        }

        slicingCam.GetComponent<SelectingItems>().enabled = false;
        isInteracting = false;
    }
}
