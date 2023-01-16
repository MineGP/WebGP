using WebGP.Interfaces;

namespace WebGP.Models;

public record Response : IResponse
{
    public Status Status => Status.Success;
}

public record Response<T>(T Result) : IResponseOrError<T>
{
    public Status Status => Status.Success;
}