public class UnityAnalyticSauces : Unity.Services.Analytics.Event
{
    //Creating the event and linking it to game services
    public UnityAnalyticSauces() : base(name: "Sauces")
    {
    }

    //How to add paramater values to event
    public string amount { set { SetParameter(name: "Amount", value); } }
    public string sauceName { set { SetParameter(name: "Sauce_Name", value); } }
    public bool isCorrect { set { SetParameter(name: "isCorrect", value); } }
}
