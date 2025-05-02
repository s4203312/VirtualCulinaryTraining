public class UnityAnalyticPlating : Unity.Services.Analytics.Event
{
    //Creating the event and linking it to game services
    public UnityAnalyticPlating() : base(name: "Plating")
    {
    }

    //How to add paramater values to event
    public bool isCorrect { set { SetParameter(name: "isCorrect", value); } }
}
