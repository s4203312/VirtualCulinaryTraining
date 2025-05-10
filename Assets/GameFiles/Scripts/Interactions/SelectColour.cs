using UnityEngine;

public class SelectColour : MonoBehaviour
{
    //Variables
    public Camera currentCam;
    public GameObject hitColour;

    private void Update()
    {
        if (Input.GetMouseButton(0))        //Chosing the colour
        {
            hitColour = ChooseColour();
        }
    }
    private GameObject ChooseColour()
    {
        GameObject hitColour = null;

        Ray ray = currentCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            if(hit.transform.tag == "ColourSelection") 
            {
                hitColour = hit.collider.gameObject;            //Finding which colour was clicked
            }
        }
        return hitColour;
    }
}
