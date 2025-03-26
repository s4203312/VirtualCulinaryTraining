using Unity.VisualScripting;
using UnityEngine;

public class MouseSlicing : MonoBehaviour
{
    public GameObject knife;
    public GameObject choppingBoard;
    public GameObject UIcanvas;
    public GameObject colourSelectionPrefab;
    private GameObject colourSelection;

    private bool isKnifeInHand = false;
    private bool isInteracting = false;

    private void Update()
    {
        //Selecting colour of board
        if(isInteracting == false && GetComponent<Camera>().enabled == true)
        {
            isInteracting = true;
            ShowColourSelection();
        }
        if(colourSelection != null && colourSelection.GetComponent<SelectColour>().hitColour != null)
        {
            GameObject hitObject = colourSelection.GetComponent<SelectColour>().hitColour;
            Material chosenMaterial = hitObject.GetComponent<Renderer>().material;
            choppingBoard.GetComponent<Renderer>().material = chosenMaterial;
            Destroy(colourSelection);

            UIcanvas.SetActive(true);
        }

        //Picking up knife
        if (isKnifeInHand)
        {
            //Functionality for knife
        }
    }

    private void ShowColourSelection()
    {
        Vector3 newPos = this.transform.position + this.transform.forward * 0.8f;
        colourSelection = Instantiate(colourSelectionPrefab, newPos, Quaternion.identity);
        colourSelection.GetComponent<SelectColour>().currentCam = GetComponent<Camera>();
    }

    public void PickUpKnife()
    {
        isKnifeInHand = true;
        UIcanvas.SetActive(false);

        knife.transform.position = this.transform.position + this.transform.forward * 0.8f;
    }

    public void DropKnife(Vector3 oldKnifePos)
    {
        if (Input.GetKey(KeyCode.E) == true)
        {
            knife.transform.position = oldKnifePos;
            isKnifeInHand = false;
        }
    }
}
