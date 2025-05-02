public class UnityAnalyticChopping : Unity.Services.Analytics.Event
{
    //Creating the event and linking it to game services
    public UnityAnalyticChopping() : base(name: "ChoppingBoard")
    {
    }

    //How to add paramater values to event
    public string boardColour { set { SetParameter(name: "Board_Colour", value); } }
    public string cutItemName { set { SetParameter(name: "Cut_Item", value); } }
    public bool isCorrect { set { SetParameter(name: "isCorrect", value); } }
}
