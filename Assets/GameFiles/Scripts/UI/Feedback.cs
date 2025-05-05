using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using System.Globalization;
using UnityEngine.SocialPlatforms;

public class Feedback : MonoBehaviour
{
    private string filePath;

    private string finalFeedback;
    public TMP_Text firstFeedbackBox;
    public TMP_Text secondFeedbackBox;

    private string incorrectName;
    private string incorrectValue;

    //Review variables
    public bool isPathFound;
    public TMP_InputField userNameInput;

    public void Start()
    {
        if (isPathFound)
        {
            filePath = FileManager.filePath;
            AnalyticsWrapper savedInfo = LoadFile();
            CreateFeedback(savedInfo);
        }
    }

    public void ReviewStart()
    {
        if (userNameInput.text != "")
        {
            filePath = Path.Combine(Application.persistentDataPath, userNameInput.text.Trim() + "Analytics.json");
            AnalyticsWrapper savedInfo = LoadFile();
            CreateFeedback(savedInfo);
        }
    }

    public AnalyticsWrapper LoadFile()
    {
        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<AnalyticsWrapper>(json);        //Deserialising the list so unity can read it again
    }

    public void CreateFeedback(AnalyticsWrapper savedInfo)
    {
        finalFeedback = "First Attempt: ";
        foreach (var anaEvent in savedInfo.FirstTryEvents)
        {
            EventSwitch(anaEvent);
        }
        firstFeedbackBox.text = finalFeedback;

        finalFeedback = "Second Attempt: ";
        foreach (var anaEvent in savedInfo.SecondTryEvents)
        {
            EventSwitch(anaEvent);
        }
        secondFeedbackBox.text = finalFeedback;
    }

    //Switch statments for retrieving events
    private void EventSwitch(AnalyticsEvent anaEvent)
    {
        for (int i = 0; i < anaEvent.eventData.Count; i++)
        {
            switch (anaEvent.eventName)
            {
                case "Chopping Board Event:":
                    ChoppingData(anaEvent, i);
                    break;
                case "Sauces Event:":
                    SaucesData(anaEvent, i);
                    break;
                case "Frying Event:":
                    FryingData(anaEvent, i);
                    break;
                case "Fridge Event:":
                    FridgeData(anaEvent, i);
                    break;
                case "Cheese Event:":
                    CheeseData(anaEvent, i);
                    break;
                case "Prepping Event:":
                    PreppingData(anaEvent, i);
                    break;
                case "Plating Event:":
                    PlatingData(anaEvent, i);
                    break;
                case "Time Taken Event:":
                    TimeTakenData(anaEvent, i);
                    break;
                default:
                    finalFeedback = "\n You completed the training perfectly with no errors well done";
                    break;
            }
            Debug.Log(finalFeedback);
        }
    }

    //Retrieving data from events
    public void ChoppingData(AnalyticsEvent anaEvent, int pos)
    {
        if (anaEvent.eventData[pos] == "False")
        {
            //Retrieving the incorrect data
            incorrectName = anaEvent.eventData[pos - 1].ToString();
            incorrectValue = anaEvent.eventData[pos - 2].ToString();

            string correctBoard;
            if (incorrectName == "MinceBeef")
            {
                correctBoard = "Red";
            }
            else
            {
                correctBoard = "Green";
            }

            finalFeedback = finalFeedback + "\n" + "You incorrectly chopped a " + incorrectName + " using a " + incorrectValue
                            + " chopping board. Next time use a " + correctBoard + " chopping board for the " + incorrectName;
        }
    }
    public void SaucesData(AnalyticsEvent anaEvent, int pos)
    {
        if (anaEvent.eventData[pos] == "False")
        {
            //Retrieving the incorrect data
            incorrectName = anaEvent.eventData[pos - 1].ToString();
            incorrectValue = anaEvent.eventData[pos - 2].ToString();

            //Add feedback
        }
    }
    public void FryingData(AnalyticsEvent anaEvent, int pos)
    {
        if (anaEvent.eventData[pos] == "False")
        {
            //Retrieving the incorrect data
            incorrectName = anaEvent.eventData[pos - 1].ToString();
            incorrectValue = anaEvent.eventData[pos - 2].ToString();

            string cookedLevel = null;
            if ((float.Parse(incorrectValue, CultureInfo.InvariantCulture.NumberFormat)) > 20)
            {
                cookedLevel = "burnt";
            }
            else if ((float.Parse(incorrectValue, CultureInfo.InvariantCulture.NumberFormat)) < 10)
            {
                cookedLevel = "under cooked";
            }

            finalFeedback = finalFeedback + "\n" + "You incorrectly cooked the " + incorrectName + " You cooked the " + incorrectName
                            + " for " + incorrectValue + " seconds this resulted in a " + cookedLevel + " " + incorrectName;
        }
    }
    public void CheeseData(AnalyticsEvent anaEvent, int pos)
    {
        if (anaEvent.eventData[pos] == "False")
        {
            finalFeedback = finalFeedback + "\n" + "You forgot to add the cheese to the burger";
        }
    }
    public void FridgeData(AnalyticsEvent anaEvent, int pos)
    {
        if (anaEvent.eventData[pos] == "False")
        {
            finalFeedback = finalFeedback + "\n" + "You forgot to close the fridge after taking the burgers out"; 
        }
    }
    public void PreppingData(AnalyticsEvent anaEvent, int pos)
    {
        if (anaEvent.eventData[pos] == "False")
        {
            finalFeedback = finalFeedback + "\n" + "You forgot to prep items that are required in the recipe";
        }
    }
    public void PlatingData(AnalyticsEvent anaEvent, int pos)
    {
        if (anaEvent.eventData[pos] == "False")
        {
            finalFeedback = finalFeedback + "\n" + "You incorrectly plated the burger";
        }
    }
    public void TimeTakenData(AnalyticsEvent anaEvent, int pos)
    {
        string timeTaken = anaEvent.eventData[pos];
        finalFeedback = finalFeedback + "\n" + "You took " + timeTaken + " seconds to complete the training";
    }
}
