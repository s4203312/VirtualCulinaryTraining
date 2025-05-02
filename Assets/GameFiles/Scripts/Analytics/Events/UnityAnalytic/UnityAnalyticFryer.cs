public class UnityAnalyticFryer : Unity.Services.Analytics.Event
{
    //Creating the event and linking it to game services
    public UnityAnalyticFryer() : base(name: "Fryers")
    {
    }

    //How to add paramater values to event
    public string cookingTime { set { SetParameter(name: "cooking_Time", value); } }
    public string itemName { set { SetParameter(name: "item_Name", value); } }
    public bool isCorrect { set { SetParameter(name: "isCorrect", value); } }
}
