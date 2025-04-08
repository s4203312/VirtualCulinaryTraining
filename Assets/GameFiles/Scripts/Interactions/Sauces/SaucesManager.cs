using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaucesManager : MonoBehaviour
{
    public Camera saucesCam;
    public GameObject mixingBowl;
    public GameObject lettuceBowl;

    public Transform prepStationMixBowlPos;

    public void CompletedSauce()
    {
        mixingBowl.transform.position = prepStationMixBowlPos.position;
        mixingBowl.transform.rotation = prepStationMixBowlPos.rotation;
    }
}
