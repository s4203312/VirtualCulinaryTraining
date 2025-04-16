using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

//Main script for analytics storage using all the events, events data and creating seperate listeners for each event
public class AnalyticsListener : MonoBehaviour
{
    //Events
    public ChoppingEvent choppingEvent;

    //Event listeners
    private GameEventListener<ChoppingEventData> choppingListener;

    private async void Awake()
    {
        await UnityServices.InitializeAsync();      //Initializing the analytic service
        AnalyticsService.Instance.StartDataCollection();    //Starting to collect data
    }

    private void OnEnable()
    {
        //Creating listeners
        choppingListener = new InlineListener<ChoppingEventData>(OnChoppingBoardUsed);

        //Registering listener to event
        choppingEvent.RegisterListener(choppingListener);
    }

    private void OnDisable()
    {
        //Removing the listeners from the events
        choppingEvent.UnregisterListener(choppingListener);
    }

    //Function for storing data when using chopping board
    private void OnChoppingBoardUsed(ChoppingEventData data)
    {
        Debug.Log(data.boardColour);
        Debug.Log(data.cutItemName);
        Debug.Log(data.isCorrect);

        //Storing data in analytic event
        var UAChopping = new UnityAnalyticChopping
        {
            boardColour = data.boardColour,
            cutItemName = data.cutItemName,
            isCorrect = data.isCorrect
        };
        AnalyticsService.Instance.RecordEvent(UAChopping);    //Recording the information

        //Storing data in JSON
        List<string> allData = new List<string>
        {
            "Chopping Board Event:",
            data.boardColour,
            data.cutItemName,
            data.isCorrect.ToString()
        };
        FileManager.SaveEvent(allData);
    }
}
