public class UnityAnalyticFridge : Unity.Services.Analytics.Event
{
    //Creating the event and linking it to game services
    public UnityAnalyticFridge() : base(name: "Fridge")
    {
    }

    //How to add paramater values to event
    public bool isCorrect { set { SetParameter(name: "isCorrect", value); } }
}
