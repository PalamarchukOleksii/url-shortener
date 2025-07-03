namespace UrlShortener.Domain.Shared;

public class Result<TValue> : Result
{
    private readonly TValue _value;

    private Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        if (isSuccess && value is null)
            throw new ArgumentNullException(nameof(value), "Value cannot be null for success results.");

        _value = value!;
    }

    public TValue Value => IsSuccess
        ? _value
        : throw new InvalidOperationException("Cannot access the value of a failure result.");

    public static Result<TValue> Success(TValue value) => new Result<TValue>(value, true, Error.None);

    public new static Result<TValue> Failure(Error error) => new Result<TValue>(default, false, error);
}