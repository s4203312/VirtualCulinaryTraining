using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public GameManager gameManager;
    public TMP_InputField userNameInput;
    private string usersName;

    //Analytic collection
    public TimeTakenEvent timeTakenEvent;

    //Changing the scene to start the game
    public void StartGame()
    {
        usersName = userNameInput.text.Trim();       //Removing spaces from end
        if(!string.IsNullOrEmpty(usersName))
        {
            //Store in playerprefs for use later
            PlayerPrefs.SetString("UsersName", usersName);
            PlayerPrefs.Save();

            SceneManager.LoadScene(1);
        }
    }

    public void LoadFeedback()
    {
        timeTakenEvent.Raise(new TimeTakenEventData { timeTaken = gameManager.overallTimeTaken });
        SceneManager.LoadScene(2);
    }
    public void RetryTraining()
    {
        SceneManager.LoadScene(1);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Exiting the application
    public void QuitGame()
    {
        Application.Quit();
    }
}
