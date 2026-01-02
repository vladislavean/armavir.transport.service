namespace core.abstractions;

public record Result
{
    protected Result(bool isSuccess, Error error)
    {
        Error = error;
        IsSuccess = isSuccess;
    }

    public Error Error { get; }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);

    public static implicit operator Result(Error error) => Failure(error);
}

public sealed record Result<TValue> : Result
{
    public Result(TValue value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = value;
    }

    private readonly TValue _value;

    public TValue Value => IsSuccess
        ? _value
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public static implicit operator Result<TValue>(TValue value) => new(value, true, Error.None);

    public static implicit operator Result<TValue>(Error error) => new(default!, false, error);
}

public sealed record Error
{
    public static readonly Error None = new(string.Empty, ErrorType.None);

    public string Message { get; }
    public ErrorType Type { get; }

    private Error(string message, ErrorType type)
    {
        Message = message;
        Type = type;
    }
    public static Error Failure(string message) => new (message, ErrorType.Failure);
}

public enum ErrorType
{
    None,
    Failure
}