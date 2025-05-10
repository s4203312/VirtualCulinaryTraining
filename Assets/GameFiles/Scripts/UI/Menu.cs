using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    //Managers
    public GameManager gameManager;

    //Variables
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
            PlayerPrefs.SetInt("FirstAttempt", 1);
            PlayerPrefs.Save();

            //File path that works with any machine not just a local path and individual to user
            FileManager.filePath = Path.Combine(Application.persistentDataPath, usersName + "Analytics.json");
            string filePath = FileManager.filePath;
            File.WriteAllText(filePath, "{}");                      //Clearing file

            SceneManager.LoadScene(1);
        }
    }

    public void LoadFeedback()      //Called when training is over the record final time
    {
        timeTakenEvent.Raise(new TimeTakenEventData { timeTaken = gameManager.overallTimeTaken });
        SceneManager.LoadScene(2);
    }
    public void RetryTraining()     //Retrying the training
    {
        PlayerPrefs.SetInt("FirstAttempt", 0);
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
