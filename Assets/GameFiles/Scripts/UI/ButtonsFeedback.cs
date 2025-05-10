using UnityEngine;

public class ButtonsFeedback : MonoBehaviour
{
    //Variables
    public GameObject retryButton;
    public GameObject menuButton;

    void Start()
    {
        if(PlayerPrefs.GetInt("FirstAttempt") == 1)     //Finding out if its the user need to retry the recipe
        {
            retryButton.SetActive(true);
            menuButton.SetActive(false);
        }
        else
        {
            retryButton.SetActive(false);
            menuButton.SetActive(true);
        }
    }
}
