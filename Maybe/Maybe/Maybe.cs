using Maybe.Interfaces;

namespace Maybe;

public abstract class Maybe<T> : IMaybe<T>
{
    public static IMaybe<T> From(T? value) => value == null ? new Nothing<T>() : new Just<T>(value);

    public abstract T Get();

    public abstract T GetOrElse(T defaultValue);

    public abstract T GetOrCall(Func<T> defaultFunc);

    public abstract T GetOrThrow(Exception exception);

    public abstract bool IsEmpty();

    public abstract bool IsNotEmpty();
}