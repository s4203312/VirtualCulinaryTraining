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
                GameObject slicedObject = collider.transform.GetComponent<SlicingObject>().slicedVersion;
                Instantiate(slicedObject, collider.transform.position, Quaternion.identity);
                Destroy(collider.gameObject);
                
            }
        }
    }
}
