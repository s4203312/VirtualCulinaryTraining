using System.IO;
using UnityEngine;
using TMPro;
using System;
using System.Globalization;
using Unity.VisualScripting;
using System.Net.Http.Headers;

public class Feedback : MonoBehaviour
{
    //Variables for Visual Feedback
    private string filePath;
    public GameObject firstTable;
    public GameObject secondTable;
    public GameObject currentTable;
    private bool hasHitFalse;
    public GameObject visualFeedbackButton;

    //Variables for Written Feedback
    private string finalFeedback;
    public TMP_Text writtenFeedbackBox;
    public GameObject writtenFeedbackButton;
    private int bulletCounter;
    private string incorrectName;
    private string incorrectValue;

    //Review variables
    public bool isPathFound;
    public TMP_InputField userNameInput;

    public void Start()
    {
        if (isPathFound)        //If there is a file then start creating feedback
        {
            filePath = FileManager.filePath;
            AnalyticsWrapper savedInfo = LoadFile();
            writtenFeedbackButton.SetActive(true);              //Sets written feedback button to active
            visualFeedbackButton.SetActive(false);
            CreateFeedback(savedInfo);
        }
    }

    public void ReviewStart()           //Used for reviewing the data outside of the training feedback
    {
        if (userNameInput.text != "")
        {
            filePath = Path.Combine(Application.persistentDataPath, userNameInput.text.Trim() + "Analytics.json");
            AnalyticsWrapper savedInfo = LoadFile();
            CreateFeedback(savedInfo);
        }
    }

    public void WrittenFeedback()           //Displaying a written version for more indepth feedback
    {
        writtenFeedbackBox.text = finalFeedback;
        visualFeedbackButton.SetActive(true);
        writtenFeedbackButton.SetActive(false);
        writtenFeedbackBox.gameObject.SetActive(true);
        firstTable.SetActive(false);
        secondTable.SetActive(false);
    }

    public void VisualFeedback()
    {
        visualFeedbackButton.SetActive(false);
        writtenFeedbackButton.SetActive(true);
        writtenFeedbackBox.gameObject.SetActive(false);
        firstTable.SetActive(true);
    }

    public AnalyticsWrapper LoadFile()
    {
        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<AnalyticsWrapper>(json);        //Deserialising the list so unity can read it again
    }

    public void CreateFeedback(AnalyticsWrapper savedInfo)      //Filling in the tables for feedback
    {
        finalFeedback = "";
        bulletCounter = 0;

        currentTable = firstTable;
        currentTable.SetActive(true);
        foreach (var anaEvent in savedInfo.FirstTryEvents)
        {
            EventSwitch(anaEvent);
        }

        if(savedInfo.SecondTryEvents.Count > 0)
        {
            if(writtenFeedbackButton != null)
            {
                writtenFeedbackButton.SetActive(false);             //Sets written feedback button to disabled as only gives written feedback after attempt one
            }
            currentTable = secondTable;
            currentTable.SetActive(true);
            foreach (var anaEvent in savedInfo.SecondTryEvents)
            {
                EventSwitch(anaEvent);
            }
        }
    }

    //Switch statments for retrieving events
    private void EventSwitch(AnalyticsEvent anaEvent)
    {
        for (int i = 0; i < anaEvent.eventData.Count; i++)
        {
            hasHitFalse = false;
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
                    break;
            }

            if (hasHitFalse)            //If an event fires false then that event has been failed and a cross will be displayed
            {
                return;
            }
        }
    }

    //Retrieving data from events
    public void ChoppingData(AnalyticsEvent anaEvent, int pos)
    {
        if (anaEvent.eventData[pos] == "True")
        {
            currentTable.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            currentTable.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        }
        else if (anaEvent.eventData[pos] == "False")
        {
            hasHitFalse = true;
            currentTable.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            currentTable.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);

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

            bulletCounter++;
            finalFeedback = finalFeedback + "\n" + bulletCounter + ". You incorrectly chopped a " + incorrectName + " using a " + incorrectValue
            + " coloured chopping board. Next time use a " + correctBoard + " chopping board.";
        }
    }
    public void SaucesData(AnalyticsEvent anaEvent, int pos)
    {
        if (anaEvent.eventData[pos] == "True")
        {
            currentTable.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            currentTable.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        }
        else if (anaEvent.eventData[pos] == "False")
        {
            hasHitFalse = true;
            currentTable.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
            currentTable.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);

            bulletCounter++;
            finalFeedback = finalFeedback + "\n" + bulletCounter + ". You incorrectly mixed the sauce ingredients. Remember to follow recipe";
        }
    }
    public void FryingData(AnalyticsEvent anaEvent, int pos)
    {
        if (anaEvent.eventData[pos] == "True")
        {
            currentTable.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
            currentTable.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
        }
        else if (anaEvent.eventData[pos] == "False")
        {
            hasHitFalse = true;
            currentTable.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
            currentTable.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);

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

            bulletCounter++;
            finalFeedback = finalFeedback + "\n" + bulletCounter + ". You incorrectly cooked the " + incorrectName + ". You cooked the " + incorrectName
            + " for " + incorrectValue + " seconds this resulted in a " + cookedLevel + " " + incorrectName;
        }
    }
    public void CheeseData(AnalyticsEvent anaEvent, int pos)
    {
        if (anaEvent.eventData[pos] == "True")
        {
            currentTable.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
            currentTable.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
        }
        else if (anaEvent.eventData[pos] == "False")
        {
            bulletCounter++;
            finalFeedback = finalFeedback + "\n" + bulletCounter + ". You forgot to add the cheese to the burger";
        }
    }
    public void FridgeData(AnalyticsEvent anaEvent, int pos)
    {
        if (anaEvent.eventData[pos] == "True")
        {
            currentTable.transform.GetChild(4).GetChild(0).gameObject.SetActive(false);
            currentTable.transform.GetChild(4).GetChild(1).gameObject.SetActive(true);
        }
        else if (anaEvent.eventData[pos] == "False")
        {
            hasHitFalse = true;
            currentTable.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
            currentTable.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);

            bulletCounter++;
            finalFeedback = finalFeedback + "\n" + bulletCounter + ". You forgot to close the fridge after taking the burgers out";
        }
    }
    public void PreppingData(AnalyticsEvent anaEvent, int pos)
    {
        if (anaEvent.eventData[pos] == "True")
        {
            currentTable.transform.GetChild(5).GetChild(0).gameObject.SetActive(false);
            currentTable.transform.GetChild(5).GetChild(1).gameObject.SetActive(true);
        }
        else if (anaEvent.eventData[pos] == "False")
        {
            bulletCounter++;
            finalFeedback = finalFeedback + "\n" + bulletCounter + ". You forgot items that are required in the recipe";
        }
    }
    public void PlatingData(AnalyticsEvent anaEvent, int pos)
    {
        if (anaEvent.eventData[pos] == "True")
        {
            currentTable.transform.GetChild(6).GetChild(0).gameObject.SetActive(false);
            currentTable.transform.GetChild(6).GetChild(1).gameObject.SetActive(true);
        }
        else if (anaEvent.eventData[pos] == "False")
        {
            bulletCounter++;
            finalFeedback = finalFeedback + "\n" + bulletCounter + ". You incorrectly plated the burger";
        }
    }
    public void TimeTakenData(AnalyticsEvent anaEvent, int pos)
    {
        string timeTaken = anaEvent.eventData[pos];
        int mins = TimeSpan.FromSeconds(Double.Parse(timeTaken)).Minutes;
        int secs = TimeSpan.FromSeconds(Double.Parse(timeTaken)).Seconds;
        currentTable.transform.GetChild(7).GetChild(0).GetComponent<TMP_Text>().text = mins.ToString() + ":" + secs.ToString();
    }
}
