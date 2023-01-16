using WebGP.Interfaces;

namespace WebGP.Models
{
    public record Error(string Message) : IResponse
    {
        public Status Status => Status.Error;
    }
    public record Error<T>(string Message) : IResponseOrError<T>
    {
        public Status Status => Status.Error;
    }
}
