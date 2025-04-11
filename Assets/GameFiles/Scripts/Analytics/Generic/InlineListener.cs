public class InlineListener<T> : GameEventListener<T>       //Helper script to use a generic listener class and make one analytics listener script
{
    private System.Action<T> callback;

    public InlineListener(System.Action<T> callback)
    {
        this.callback = callback;
    }

    public override void OnEventRaised(T data)
    {
        callback?.Invoke(data);
    }
}
