namespace Maybe.Interfaces;

public interface IMaybe<T>
{
    public T Get();

    public T GetOrElse(T defaultValue);

    public T GetOrCall(Func<T> defaultFunc);

    public Task<T> GetOrCallAsync(Func<Task<T>> defaultFunc);

    public T GetOrThrow(Exception exception);

    public bool IsValid();

    public bool IsInvalid();
}