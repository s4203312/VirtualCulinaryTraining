using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectColour : MonoBehaviour
{
    public Camera currentCam;
    public GameObject hitColour;

    private void Update()
    {
        if (Input.GetMouseButton(0))
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
                Debug.Log(hit.transform.name);
                hitColour = hit.collider.gameObject;
            }
        }
        return hitColour;
    }
}
