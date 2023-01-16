namespace WebGP.Interfaces;

public enum Status
{
    Error,
    Success
}

public interface IResponse
{
    Status Status { get; }
}

public interface IResponseOrError<T> : IResponse
{
}