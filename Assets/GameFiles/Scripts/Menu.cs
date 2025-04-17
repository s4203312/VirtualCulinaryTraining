using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public TMP_InputField userNameInput;
    private string usersName;

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

    //Exiting the application
    public void QuitGame()
    {
        Application.Quit();
    }
}
