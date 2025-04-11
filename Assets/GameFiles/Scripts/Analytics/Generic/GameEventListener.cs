public abstract class GameEventListener<T>          //Generic class for listeners
{
    public abstract void OnEventRaised(T data);
}
