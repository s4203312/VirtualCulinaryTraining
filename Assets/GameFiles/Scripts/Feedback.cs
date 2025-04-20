using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class Feedback : MonoBehaviour
{
    private string filePath;

    private string finalFeedback;
    public TMP_Text feedbackBox;

    private string incorrectName;
    private string incorrectValue;

    public void Start()
    {
        filePath = FileManager.filePath;
        AnalyticsWrapper savedInfo = LoadFile();
        CreateFeedback(savedInfo);
        ProvideFeedback();
    }

    private AnalyticsWrapper LoadFile()
    {
        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<AnalyticsWrapper>(json);        //Deserialising the list so unity can read it again
    }

    private void CreateFeedback(AnalyticsWrapper savedInfo)
    {
        foreach(var anaEvent in savedInfo.events)
        {
            for (int i = 0; i < anaEvent.eventData.Count; i++)
            {    
                if(anaEvent.eventName == "Chopping Board Event:")
                {
                    if (anaEvent.eventData[i] == "False")
                    {
                        //Retrieving the incorrect data
                        incorrectName = anaEvent.eventData[i - 1].ToString();
                        incorrectValue = anaEvent.eventData[i - 2].ToString();

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
                if (anaEvent.eventName == "Sauces Event:")
                {
                    if (anaEvent.eventData[i] == "False")
                    {
                        //Retrieving the incorrect data
                        incorrectName = anaEvent.eventData[i - 1].ToString();
                        incorrectValue = anaEvent.eventData[i - 2].ToString();

                        //Add feedback
                    }
                }
            }
        }
    }

    private void ProvideFeedback()
    {
        //Try doing some resizing stuff here to ensure text fits
        feedbackBox.text = finalFeedback;
    }
}
