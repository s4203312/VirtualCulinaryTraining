using System.IO;
using UnityEngine;
using TMPro;
using System;

public class Feedback : MonoBehaviour
{
    //Variables
    private string filePath;
    public GameObject firstTable;
    public GameObject secondTable;
    public GameObject currentTable;
    private bool hasHitFalse;
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

    public AnalyticsWrapper LoadFile()
    {
        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<AnalyticsWrapper>(json);        //Deserialising the list so unity can read it again
    }

    public void CreateFeedback(AnalyticsWrapper savedInfo)      //Filling in the tables for feedback
    {
        currentTable = firstTable;
        currentTable.SetActive(true);
        foreach (var anaEvent in savedInfo.FirstTryEvents)
        {
            EventSwitch(anaEvent);
        }

        if(savedInfo.SecondTryEvents.Count > 0)
        {
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
        }
    }
    public void CheeseData(AnalyticsEvent anaEvent, int pos)
    {
        if (anaEvent.eventData[pos] == "True")
        {
            currentTable.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
            currentTable.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
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
        }
    }
    public void PreppingData(AnalyticsEvent anaEvent, int pos)
    {
        if (anaEvent.eventData[pos] == "True")
        {
            currentTable.transform.GetChild(5).GetChild(0).gameObject.SetActive(false);
            currentTable.transform.GetChild(5).GetChild(1).gameObject.SetActive(true);
        }
    }
    public void PlatingData(AnalyticsEvent anaEvent, int pos)
    {
        if (anaEvent.eventData[pos] == "True")
        {
            currentTable.transform.GetChild(6).GetChild(0).gameObject.SetActive(false);
            currentTable.transform.GetChild(6).GetChild(1).gameObject.SetActive(true);
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
