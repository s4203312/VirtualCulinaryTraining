using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileManager
{
    //Path for file
    public static string filePath;
    public static bool foundEvent;

    //Function for saving the new event to the JSON file
    public static void SaveEvent(string eventName, List<string> eventData)
    {
        AnalyticsWrapper wrapper = LoadFile();      //Finding the current data file
        foundEvent = false;

        if (PlayerPrefs.GetInt("FirstAttempt") == 1)        //First attempt
        {
            foreach (var anaEvent in wrapper.FirstTryEvents)    //Checking if we have an event log already in the file
            {
                if (anaEvent.eventName == eventName)
                {
                    anaEvent.eventData.AddRange(eventData);          //Adding the new event data to the dictionary
                    foundEvent = true;
                }
            }
            if (!foundEvent)                        //If event cant be found create a new list
            {
                wrapper.FirstTryEvents.Add(new AnalyticsEvent
                {
                    eventName = eventName,
                    eventData = new List<string>(eventData)
                });
            }
        }
        else                            //Second attempt
        {
            foreach (var anaEvent in wrapper.SecondTryEvents)    //Checking if we have an event log already in the file
            {
                if (anaEvent.eventName == eventName)
                {
                    anaEvent.eventData.AddRange(eventData);          //Adding the new event data to the dictionary
                    foundEvent = true;
                }
            }
            if (!foundEvent)                        //If event cant be found create a new list
            {
                wrapper.SecondTryEvents.Add(new AnalyticsEvent
                {
                    eventName = eventName,
                    eventData = new List<string>(eventData)
                });
            }
        }

        string json = JsonUtility.ToJson(wrapper, true);        //Converting to JSON using the serialised the list

        File.WriteAllText(filePath, json);
    }

    public static AnalyticsWrapper LoadFile()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<AnalyticsWrapper>(json);        //Deserialising the list so unity can read it again
        }
        else
        {
            return new AnalyticsWrapper { FirstTryEvents = new List<AnalyticsEvent>() };        //Creating a new section if the events section cant be found
        }
    }
}


[System.Serializable]       
public class AnalyticsWrapper   //Serialising the list so it can be converted to JSON
{ 
    public List<AnalyticsEvent> FirstTryEvents = new List<AnalyticsEvent>(); 
    public List<AnalyticsEvent> SecondTryEvents = new List<AnalyticsEvent>(); 
}

[System.Serializable]
public class AnalyticsEvent     //Same as a dictionary but can be used in JSON format
{
    public string eventName;
    public List<string> eventData;
}
