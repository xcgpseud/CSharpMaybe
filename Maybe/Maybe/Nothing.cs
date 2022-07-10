using Maybe.Exceptions;

namespace Maybe;

public class Nothing<T> : Maybe<T>
{
    public override T Get() => throw new MaybeValueNotFoundException();

    public override T GetOrElse(T defaultValue) => defaultValue;

    public override T GetOrCall(Func<T> defaultFunc) => defaultFunc();

    public override async Task<T> GetOrCallAsync(Func<Task<T>> defaultFunc) => await defaultFunc();

    public override T GetOrThrow(Exception exception) => throw exception;

    public override bool IsEmpty() => true;

    public override bool IsNotEmpty() => false;
}