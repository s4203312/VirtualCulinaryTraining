using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseSlicing : MonoBehaviour
{
    public GameObject knife;

    private bool isKnifeInHand = false;


    private void Update()
    {
        //Picking up knife
        if (isKnifeInHand)
        {
            //knife.transform.position = (this.transform.position + this.transform.forward * 0.8f);
            Debug.Log("Moving knife");
        }
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
