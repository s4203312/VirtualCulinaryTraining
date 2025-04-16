using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileManager
{
    //File path that works with any machine not just a local path
    private static string filePath => Path.Combine(Application.persistentDataPath, "GameAnalytics.json");

    //Function for saving the new event to the JSON file
    public static void SaveEvent(List<string> eventData)
    {
        AnalyticsWrapper wrapper = LoadFile();      //Finding the current data file
        wrapper.events.AddRange(eventData);         //Adding the new data to the file  !using add range to combine the lists

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
            return new AnalyticsWrapper { events = new List<string>() };
        }
    }
}


[System.Serializable]       //Serialising the list so it can be converted to JSON
public class AnalyticsWrapper { public List<string> events = new List<string>(); }
