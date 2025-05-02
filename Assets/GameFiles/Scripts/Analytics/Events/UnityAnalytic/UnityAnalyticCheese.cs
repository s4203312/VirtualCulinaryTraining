public class UnityAnalyticCheese : Unity.Services.Analytics.Event
{
    //Creating the event and linking it to game services
    public UnityAnalyticCheese() : base(name: "Cheese")
    {
    }

    //How to add paramater values to event
    public bool isCorrect { set { SetParameter(name: "isCorrect", value); } }
}
