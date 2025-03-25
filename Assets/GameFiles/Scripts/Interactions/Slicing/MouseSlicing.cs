using UnityEngine;

public class MouseSlicing : MonoBehaviour
{
    public GameObject knife;
    private bool isKnifeInHand = false;

    private void Update()
    {
        if (isKnifeInHand)
        {
            Vector3 lookAtPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z) + transform.forward;
            knife.transform.position = GetComponent<Camera>().ScreenToWorldPoint(lookAtPoint);
        }
    }

    public void PickUpKnife()
    {
        isKnifeInHand = true;
        //return knife.transform.position;
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
