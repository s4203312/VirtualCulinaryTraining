using UnityEngine;

[DisallowMultipleComponent]
public class SliceableObject : MonoBehaviour
{
    public GameObject slicedVersion;            //Storing the sliced verison of item
    public bool hasBeenSliced = false;
}
