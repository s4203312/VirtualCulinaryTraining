using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepManager : MonoBehaviour
{
    public List<GameObject> preppedIngredients = new List<GameObject>();
    public GameObject prepSection;


    void Update()
    {
        StartCoroutine(UpdateIngredients());        //Only run the update every 2 seconds to reduce constant updates
    }

    private IEnumerator UpdateIngredients()
    {
        yield return new WaitForSeconds(2f);
        int i = 0;
        foreach(GameObject preppedIngredient in preppedIngredients)
        {
            preppedIngredient.transform.position = prepSection.transform.GetChild(i).position;
            preppedIngredient.SetActive(true);
            i++;
        }

        yield return null;
    }
}
