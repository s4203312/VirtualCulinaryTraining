public class UnityAnalyticTimeTaken : Unity.Services.Analytics.Event
{
    //Creating the event and linking it to game services
    public UnityAnalyticTimeTaken() : base(name: "TimeTaken")
    {
    }

    //How to add paramater values to event
    public float timeTaken { set { SetParameter(name: "Time_Taken", value); } }
}
