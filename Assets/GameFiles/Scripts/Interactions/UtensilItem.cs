using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtensilItem : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (transform.name)
            {
                case "Knife":
                    KnifeAction();
                    break;
                case "MeasuringSpoon":          //More cases to add

                    break;
                default:
                    break;
            }
        }
    }



    public void KnifeAction()
    {
        Collider[] collidedObject = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (Collider collider in collidedObject)
        {
            if(collider.transform.tag == "PickUpFood")      //Chopping food
            {
                if(collider.transform.TryGetComponent<SliceableObject>(out SliceableObject sliceableObject))
                {
                    GameObject slicedObject = sliceableObject.slicedVersion;

                    //Changing the look of the object
                    collider.transform.GetComponent<MeshFilter>().mesh = slicedObject.GetComponent<MeshFilter>().mesh;
                    collider.transform.GetComponent<MeshRenderer>().material = slicedObject.GetComponent<MeshRenderer>().material;
                    collider.transform.localScale = slicedObject.transform.localScale;

                    collider.transform.GetComponent<MeshCollider>().sharedMesh = null;  // First, clear the old mesh
                    collider.transform.GetComponent<MeshCollider>().sharedMesh = collider.GetComponent<MeshFilter>().mesh;

                    //Setting has chopped to be true
                    sliceableObject.hasBeenSliced = true;
                }
            }
        }
    }
}
