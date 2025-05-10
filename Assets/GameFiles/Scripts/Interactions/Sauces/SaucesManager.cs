using UnityEngine;

public class SaucesManager : MonoBehaviour
{
    //Managers
    public PlatingManager platingManager;

    //Variables
    public Camera saucesCam;
    public GameObject mixingBowl;
    public GameObject lettuceBowl;
    public Transform prepStationMixBowlPos;

    public void CompletedSauce()        //Sending sauce to prep section
    {
        mixingBowl.transform.position = prepStationMixBowlPos.position;
        mixingBowl.transform.rotation = prepStationMixBowlPos.rotation;

        platingManager.preppedIngredients.Add(mixingBowl);
    }
}
