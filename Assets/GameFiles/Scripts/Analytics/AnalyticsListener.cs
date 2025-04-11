using UnityEngine;

//Main script for analytics storage using all the events, events data and creating seperate listeners for each event
public class AnalyticsListener : MonoBehaviour
{
    public ChoppingEvent choppingEvent;

    private GameEventListener<ChoppingEventData> choppingListener;

    private void OnEnable()
    {
        choppingListener = new InlineListener<ChoppingEventData>(OnChoppingBoardUsed);

        choppingEvent.RegisterListener(choppingListener);
    }

    private void OnDisable()
    {
        choppingEvent.UnregisterListener(choppingListener);
    }

    private void OnChoppingBoardUsed(ChoppingEventData data)
    {
        Debug.Log(data.boardColour);
        Debug.Log(data.cutItemName);
        Debug.Log(data.isCorrect);
        //UnityEngine.Analytics.Analytics.CustomEvent("ChoppingBoardUsed", new()
        //{
        //    { "BoardColor", data.boardColor },
        //    { "ItemName", data.itemName },
        //    { "IsCorrect", data.isCorrect }
        //});
    }
}
