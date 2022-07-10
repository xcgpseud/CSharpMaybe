namespace Maybe;

public class Just<T> : Maybe<T>
{
    private readonly T _innerValue;

    public Just(T value)
    {
        _innerValue = value;
    }

    public override T Get() => _innerValue;

    public override T GetOrElse(T defaultValue) => _innerValue;

    public override T GetOrCall(Func<T> defaultFunc) => _innerValue;
    public override async Task<T> GetOrCallAsync(Func<Task<T>> defaultFunc) => await Task.FromResult(_innerValue);

    public override T GetOrThrow(Exception exception) => _innerValue;

    public override bool IsValid() => true;

    public override bool IsInvalid() => false;
}