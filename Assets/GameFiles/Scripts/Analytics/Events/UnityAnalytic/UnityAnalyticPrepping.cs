public class UnityAnalyticPrepping : Unity.Services.Analytics.Event
{
    //Creating the event and linking it to game services
    public UnityAnalyticPrepping() : base(name: "Prepping")
    {
    }

    //How to add paramater values to event
    public bool isCorrect { set { SetParameter(name: "isCorrect", value); } }
}
