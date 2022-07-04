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

    public override T GetOrThrow(Exception exception) => _innerValue;

    public override bool IsEmpty() => false;

    public override bool IsNotEmpty() => true;
}