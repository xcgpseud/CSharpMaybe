using System.Runtime.CompilerServices;
using Maybe.Interfaces;

namespace Maybe;

public abstract class Maybe<T> : IMaybe<T>
{
    // This sig catches from(value) | from(value, func) | from(value, func, invalidValues)
    public static IMaybe<T> From(T value, Func<T, bool>? invalidityCheck = null, params T[] invalidValues)
    {
        if (
            value == null ||
            IsInvalidAsPerCallback(value, invalidityCheck) ||
            IsInvalidAsPerInvalidValues(value, invalidValues)
        )
        {
            return new Nothing<T>();
        }

        return new Just<T>(value);
    }

    // This sig catches from(value, invalidValues)
    public static IMaybe<T> From(T value, params T[] invalidValues)
    {
        if (value == null || IsInvalidAsPerInvalidValues(value, invalidValues))
        {
            return new Nothing<T>();
        }

        return new Just<T>(value);
    }

    private static bool IsInvalidAsPerCallback(T value, Func<T, bool>? invalidityCheck)
    {
        return invalidityCheck != null && invalidityCheck(value);
    }

    private static bool IsInvalidAsPerInvalidValues(T value, params T[] invalidValues)
    {
        return invalidValues.Length > 0 && invalidValues.ToList().Contains(value);
    }

    public abstract T Get();

    public abstract T GetOrElse(T defaultValue);

    public abstract T GetOrCall(Func<T> defaultFunc);

    public abstract Task<T> GetOrCallAsync(Func<Task<T>> defaultFunc);

    public abstract T GetOrThrow(Exception exception);

    public abstract bool IsValid();

    public abstract bool IsInvalid();
}