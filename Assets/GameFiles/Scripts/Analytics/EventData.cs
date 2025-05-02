[System.Serializable]
public struct ChoppingEventData //Data being stored for the chopping event
{
    public string boardColour;
    public string cutItemName;
    public bool isCorrect;
}

public struct SaucesEventData   //Data being stored for the sauces event
{
    public string amount;
    public string sauceName;
    public bool isCorrect;
}

public struct FryingEventData   //Data being stored for the frying event
{
    public string cookingTime;
    public string itemName;
    public bool isCorrect;
}

public struct CheeseEventData   //Data being stored for correctly remembering cheese
{
    public bool isCorrect;
}

public struct FridgeEventData   //Data being stored for correctly closing fridge
{
    public bool isCorrect;
}

public struct PreppingEventData   //Data being stored for correctly prepping all items
{
    public bool isCorrect;
}

public struct PlatingEventData   //Data being stored for correctly plating the burger
{
    public bool isCorrect;
}

public struct TimeTakenEventData   //Data being stored for time taken to complete game
{
    public float timeTaken;
}
