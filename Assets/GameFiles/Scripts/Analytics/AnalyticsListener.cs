using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

//Main script for analytics storage using all the events, events data and creating seperate listeners for each event
public class AnalyticsListener : MonoBehaviour
{
    //Events
    public ChoppingEvent choppingEvent;
    public SaucesEvent saucesEvent;
    public FryingEvent fryingEvent;
    public FridgeEvent fridgeEvent;
    public CheeseEvent cheeseEvent;
    public PreppingEvent preppingEvent;
    public PlatingEvent platingEvent;
    public TimeTakenEvent timeTakenEvent;

    //Event listeners
    private GameEventListener<ChoppingEventData> choppingListener;
    private GameEventListener<SaucesEventData> saucesListener;
    private GameEventListener<FryingEventData> fryingListener;
    private GameEventListener<FridgeEventData> fridgeListener;
    private GameEventListener<CheeseEventData> cheeseListener;
    private GameEventListener<PlatingEventData> platingListener;
    private GameEventListener<PreppingEventData> preppingListener;
    private GameEventListener<TimeTakenEventData> timeTakenListener;

    private async void Awake()
    {
        await UnityServices.InitializeAsync();      //Initializing the online analytic service
        AnalyticsService.Instance.StartDataCollection();    //Starting to collect data
    }

    private void OnEnable()
    {
        //Creating listeners
        choppingListener = new InlineListener<ChoppingEventData>(OnChoppingBoardUsed);
        saucesListener = new InlineListener<SaucesEventData>(OnSpoonUsed);
        fryingListener = new InlineListener<FryingEventData>(OnFryerUsed);
        fridgeListener = new InlineListener<FridgeEventData>(OnFridgeUsed);
        cheeseListener = new InlineListener<CheeseEventData>(OnCheeseUsed);
        preppingListener = new InlineListener<PreppingEventData>(OnPreppingUsed);
        platingListener = new InlineListener<PlatingEventData>(OnPlatingUsed);
        timeTakenListener = new InlineListener<TimeTakenEventData>(OnTimeTaken);

        //Registering listener to event
        choppingEvent.RegisterListener(choppingListener);
        saucesEvent.RegisterListener(saucesListener);
        fryingEvent.RegisterListener(fryingListener);
        fridgeEvent.RegisterListener(fridgeListener);
        cheeseEvent.RegisterListener(cheeseListener);
        preppingEvent.RegisterListener(preppingListener);
        platingEvent.RegisterListener(platingListener);
        timeTakenEvent.RegisterListener(timeTakenListener);
    }

    private void OnDisable()
    {
        //Removing the listeners from the events
        choppingEvent.UnregisterListener(choppingListener);
        saucesEvent.UnregisterListener(saucesListener);
        fryingEvent.UnregisterListener(fryingListener);
        fridgeEvent.UnregisterListener(fridgeListener);
        cheeseEvent.UnregisterListener(cheeseListener);
        preppingEvent.UnregisterListener(preppingListener);
        platingEvent.UnregisterListener(platingListener);
        timeTakenEvent.UnregisterListener(timeTakenListener);
    }

    //Function for storing data when using chopping board
    private void OnChoppingBoardUsed(ChoppingEventData data)
    {
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
            data.boardColour,
            data.cutItemName,
            data.isCorrect.ToString()
        };
        FileManager.SaveEvent("Chopping Board Event:", allData);
    }

    //Function for storing data when using spoons
    private void OnSpoonUsed(SaucesEventData data)
    {
        //Storing data in analytic event
        var UASauces = new UnityAnalyticSauces
        {
            amount = data.amount,
            sauceName = data.sauceName,
            isCorrect = data.isCorrect
        };
        AnalyticsService.Instance.RecordEvent(UASauces);    //Recording the information

        //Storing data in JSON
        List<string> allData = new List<string>
        {
            data.amount,
            data.sauceName,
            data.isCorrect.ToString()
        };
        FileManager.SaveEvent("Sauces Event:", allData);
    }

    //Function for storing data when using fryers
    private void OnFryerUsed(FryingEventData data)
    {
        //Storing data in analytic event
        var UAFryer = new UnityAnalyticFryer
        {
            cookingTime = data.cookingTime,
            itemName = data.itemName,
            isCorrect = data.isCorrect
        };
        AnalyticsService.Instance.RecordEvent(UAFryer);    //Recording the information

        //Storing data in JSON
        List<string> allData = new List<string>
        {
            data.cookingTime,
            data.itemName,
            data.isCorrect.ToString()
        };
        FileManager.SaveEvent("Frying Event:", allData);
    }

    //Function for storing data when using fridge
    private void OnFridgeUsed(FridgeEventData data)
    {
        //Storing data in analytic event
        var UAFridge = new UnityAnalyticFridge
        {
            isCorrect = data.isCorrect
        };
        AnalyticsService.Instance.RecordEvent(UAFridge);    //Recording the information

        //Storing data in JSON
        List<string> allData = new List<string>
        {
            data.isCorrect.ToString()
        };
        FileManager.SaveEvent("Fridge Event:", allData);
    }

    //Function for storing data when using cheese
    private void OnCheeseUsed(CheeseEventData data)
    {
        //Storing data in analytic event
        var UACheese = new UnityAnalyticCheese
        {
            isCorrect = data.isCorrect
        };
        AnalyticsService.Instance.RecordEvent(UACheese);    //Recording the information

        //Storing data in JSON
        List<string> allData = new List<string>
        {
            data.isCorrect.ToString()
        };
        FileManager.SaveEvent("Cheese Event:", allData);
    }

    //Function for storing data when prepping
    private void OnPreppingUsed(PreppingEventData data)
    {
        //Storing data in analytic event
        var UAPrepping = new UnityAnalyticPrepping
        {
            isCorrect = data.isCorrect
        };
        AnalyticsService.Instance.RecordEvent(UAPrepping);    //Recording the information

        //Storing data in JSON
        List<string> allData = new List<string>
        {
            data.isCorrect.ToString()
        };
        FileManager.SaveEvent("Prepping Event:", allData);
    }

    //Function for storing data when plating
    private void OnPlatingUsed(PlatingEventData data)
    {
        //Storing data in analytic event
        var UAPlating = new UnityAnalyticPlating
        {
            isCorrect = data.isCorrect
        };
        AnalyticsService.Instance.RecordEvent(UAPlating);    //Recording the information

        //Storing data in JSON
        List<string> allData = new List<string>
        {
            data.isCorrect.ToString()
        };
        FileManager.SaveEvent("Plating Event:", allData);
    }

    //Function for storing time taken to complete game
    private void OnTimeTaken(TimeTakenEventData data)
    {
        //Storing data in analytic event
        var UATimeTaken = new UnityAnalyticTimeTaken
        {
            timeTaken = data.timeTaken
        };
        AnalyticsService.Instance.RecordEvent(UATimeTaken);    //Recording the information

        //Storing data in JSON
        List<string> allData = new List<string>
        {
            data.timeTaken.ToString()
        };
        FileManager.SaveEvent("Time Taken Event:", allData);
    }
}
